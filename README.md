Casual Basic
============

![Screenshot](/screenshot.png?raw=true)

Casual Basic is a Visual Studio extension for removing All Those CapitalLetters From VB.Net Code Which Are Not Necessary To The Compiler But ThereIs No Way You Can Turn Them Off. 

It doesn't actually make any changes to your source code; that still gets auto-capitalised and saved on disk with EndIfs intact. All the extension does is draw uncapitalised text on top of it in your editor.

Installation
============

Download and open the .vsix file, or build it yourself if you like. 

Caveats
=======

- None, this is an excellent idea.
- When I opened a 35,000 line generated .vb file and scrolled around a bunch, it did seem a little slower than usual? Normal source files are fine.
- "No configuration required", which means you can't edit hardcoded colors and exclusion lists unless you edit the source.
