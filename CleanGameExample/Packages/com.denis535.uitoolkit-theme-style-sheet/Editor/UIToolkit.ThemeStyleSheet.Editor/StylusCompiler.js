"use strict";
const Process = require('node:process');
const FS = require("fs");
const Path = require("path");
const Stylus = require(require.resolve("stylus", { paths: [Path.join(process.env.APPDATA, "/npm/node_modules")] }));
Stylus.nodes.Ident = new Proxy(Stylus.nodes.Ident, {
    construct: function (target, args) {
        args[0] = args[0].replaceAll(/(__)(_+)/g, '__');
        args[0] = args[0].replaceAll(/(--)(-+)/g, '--');
        return new target(...args);
    }
});

const src = Process.argv[2];
const dist = Process.argv[3];
if (src == null) throw new TypeError("Argument 'src' is required");
if (dist == null) throw new TypeError("Argument 'dist' is required");

Stylus(readStylus(src))
    .set('filename', Path.basename(src))
    .set('paths', [Path.dirname(src)])
    .define('eval', evalEx, false)
    .define('raw-eval', rawEvalEx, true)
    .define('get-string', getStringEx, true)
    .define('get-type', getTypeEx, true)
    .define('define', defineEx, true)
    .define('is-defined', isDefinedEx, true)
    .define('get-definition', getDefinitionEx, true)
    .define('get-definitions', getDefinitionsEx, true)
    .render(onComplete);

// onCallback
function onComplete(error, result) {
    if (error) {
        console.error(error);
        FS.writeFile(dist, '', onError);
    } else {
        result = result.replaceAll('.{skin}', '');
        result = result.replaceAll('.{state}', '');
        FS.writeFile(dist, result, onError);
    }
}
function onError(error) {
    if (error) {
        console.error(error);
    }
}

// readStylus
function readStylus(path) {
    return FS.readFileSync(path, 'utf8')
        // // string: ***
        .replaceAll(/(?<!\/\/.*)(\/\/\s*string:s*)(.*)/gm, function (match, comment, content) {
            content = content
                .trim()
                .replaceAll(/(\s+)/g, ' ') // collapse whitespace
                .replaceAll(/(__+)/g, '__') // collapse __
                .replaceAll(/(--+)/g, '--'); // collapse --
            return '\'' + content + '\'';
        })
        // // styles: ***
        .replaceAll(/(?<!\/\/.*)(\/\/\s*styles:\s*)(.*)/gm, function (match, comment, content) {
            return '\r\n' + '    ' + content;
        })
        // ).
        .replaceAll(/(?<!\/\/.*)(?<=\))(\.)/gm, function (match, content) {
            return '\r\n' + '    ';
        })
        // .selector--***
        .replaceAll(/(?<!\/\/.*)(?:\.selector--)([\w-.#:]+)/gm, function (match, content) {
            content = content
                .trim()
                .replaceAll(/(__+)/g, '__') // collapse __
                .replaceAll(/(--+)/g, '--'); // collapse --
            return '{get-selector(' + ('\'' + content + '\'') + ')}';
        });
}

// extensions
function evalEx(script, arg, arg2, arg3, arg4, arg5, arg6) {
    return eval(script.val);
}
function rawEvalEx(script, arg, arg2, arg3, arg4, arg5, arg6) {
    return eval(script.nodes[0].val);
}

// extensions
function getStringEx(expr) {
    expr = Stylus.utils.unwrap(expr);
    if (expr.nodes.length == 0) {
        return new Stylus.nodes.Null();
    }
    if (expr.nodes.length == 1) {
        const value = expr.nodes[0];
        if (value.constructor.name == 'String') return new Stylus.nodes.String(value.string ?? value.toString());
        if (value.constructor.name == 'Literal') return new Stylus.nodes.String(value.string ?? value.toString());
        if (value.constructor.name == 'Ident') return new Stylus.nodes.String(value.string ?? value.toString());
        if (value.constructor.name == 'Unit') return new Stylus.nodes.String(value.string ?? value.toString());
        if (value.constructor.name == 'RGBA') return new Stylus.nodes.String(value.name);
        if (value.constructor.name == 'Function') return new Stylus.nodes.String(value.name);
        if (value.constructor.name == 'Null') return new Stylus.nodes.Null();
        throw new Error("Argument is invalid: " + value);
    }
    throw new Error("Argument is invalid: " + expr);
}
function getTypeEx(expr) {
    expr = Stylus.utils.unwrap(expr);
    if (expr.nodes.length == 0) {
        return new Stylus.nodes.Null();
    }
    if (expr.nodes.length == 1) {
        const value = expr.nodes[0];
        return new Stylus.nodes.String(value.constructor.name);
    }
    {
        return new Stylus.nodes.String(expr.constructor.name);
    }
}

// extensions
function defineEx(name, expr, global) {
    name = Stylus.utils.unwrap(name).nodes[0].string;
    expr = Stylus.utils.unwrap(expr);
    global = global && global.toBoolean().isTrue;
    if (global === true) {
        const node = new Stylus.nodes.Ident(name, expr);
        this.global.scope.add(node);
        return;
    }
    if (global === false || global === undefined) {
        const node = new Stylus.nodes.Ident(name, expr);
        this.currentScope.add(node);
        return;
    }
}
function isDefinedEx(name, global) {
    name = Stylus.utils.unwrap(name).nodes[0].string;
    global = global && global.toBoolean().isTrue;
    if (global === true) {
        return new Stylus.nodes.Boolean(this.global.scope.locals[name]);
    }
    if (global === false) {
        return new Stylus.nodes.Boolean(this.currentScope.locals[name]);
    }
    return new Stylus.nodes.Boolean(this.lookup(name));
}
function getDefinitionEx(name, global) {
    name = Stylus.utils.unwrap(name).nodes[0].string;
    global = global && global.toBoolean().isTrue;
    if (global === true) {
        return this.global.scope.locals[name];
    }
    if (global === false) {
        return this.currentScope.locals[name];
    }
    return this.lookup(name);
}
function getDefinitionsEx(global) {
    global = global && global.toBoolean().isTrue;
    if (global === true) {
        const result = new Stylus.nodes.Object();
        for (const [key, value] of Object.entries(this.global.scope.locals)) {
            result.setValue(key, value);
        }
        return result;
    }
    if (global === false) {
        const result = new Stylus.nodes.Object();
        for (const [key, value] of Object.entries(this.currentScope.locals)) {
            result.setValue(key, value);
        }
        return result;
    }
    throw new Error("Argument 'global' must be defined");
}
