# USS Overview
https://docs.unity3d.com/Manual/UIE-USS.html
https://docs.unity3d.com/Manual/UIE-USS-Properties-Reference.html

# USS Selectors
## Simple selectors
Type
#name
.class
*

## Complex selectors
.a > .b - match child
.a .b - match descendant
.a.b - match if all
.a, .b - match if any

## Pseudo-states
:root
:active
:inactive
:enabled
:disabled
:focus
:hover
:selected
:checked

# USS Properties
all

## View
display
visibility
opacity

overflow
-unity-overflow-clip-box

## View/Cursor
cursor

## View/Foreground
color

-unity-font
-unity-font-definition
font-size
-unity-font-style

-unity-text-align
-unity-text-outline
-unity-text-outline-width
-unity-text-outline-color
text-shadow

text-overflow
-unity-text-overflow-position

white-space
letter-spacing
word-spacing
-unity-paragraph-spacing

## View/Background
background-color
background-image
-unity-background-image-tint-color
-unity-background-scale-mode
-unity-slice-scale
-unity-slice-left
-unity-slice-right
-unity-slice-top
-unity-slice-bottom

## View/Border
border-color
border-top-color
border-bottom-color
border-left-color
border-right-color

border-radius
border-top-left-radius
border-top-right-radius
border-bottom-left-radius
border-bottom-right-radius

## Layout/Position
position
top
bottom
left
right

## Layout/Size
width
min-width
max-width

height
min-height
max-height

## Layout/Box
margin
margin-top
margin-bottom
margin-left
margin-right

border-width
border-top-width
border-bottom-width
border-left-width
border-right-width

padding
padding-top
padding-bottom
padding-left
padding-right

## Flex/Container
flex-direction
flex-wrap
justify-content - justify children along of main-axis (start/end)
align-items - align children on cross-axis (around of main-axis) (left/right)
align-content - align children on cross-axis (around of main-axis) (left/right)

## Flex/Self
flex
flex-grow
flex-shrink
flex-basis
align-self

## Transform
transform-origin
translate
rotate
scale

## Transition
transition
transition-delay
transition-duration
transition-timing-function
transition-property
