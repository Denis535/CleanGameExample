"use strict";
const Process = require('node:process');
const FS = require('fs');
const Path = require('path');
const Pug = require(require.resolve('pug', { paths: [Path.join(process.env.APPDATA, '/npm/node_modules')] }));

const src = Process.argv[2];
const dist = Process.argv[3];
if (src == null) throw new TypeError("Source path is required");
if (dist == null) throw new TypeError("Distribution path is required");

Pug.render(FS.readFileSync(src, 'utf8'), { doctype: 'xml', pretty: true }, onComplete);

// onCallback
function onComplete(error, result) {
    if (error) {
        console.error(error);
        FS.writeFile(dist, '', onError);
    } else {
        FS.writeFile(dist, result.replaceAll('::', '.'), onError);
    }
}
function onError(error) {
    if (error) {
        console.error(error);
    }
}
