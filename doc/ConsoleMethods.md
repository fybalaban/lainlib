# lainlib docs || ConsoleMethods.cs
This document contains examples on how to use some methods that was implemented in ConsoleMethods.cs 
<br> **This document contains HTML color tags that highlight important details, make sure you can view this document with colors**

## <span style="color:#129475">print</span>(<span style="color:purple">string</span> format, <span style="color:purple">char</span> cBracket, <span style="color:purple">ConsoleColor</span> fColor)
- Example: <code>print("[Hey!] Words wrapped in [square brackets] will be [colored!]", '[', ConsoleColor.Blue);</code>
<br>Output: <span style="color:blue">Hey!</span> Words wrapped in <span style="color:blue"> square brackets</span> will be <span style="color:blue">colored!</span>
- - *Note:  Same override can be used with curly brackets as well.*
- Example: <code>print("{Hey!} Words wrapped in {curly brackets} will be {colored!}", '{', ConsoleColor.Magenta);</code>
<br>Output: <span style="color:magenta">Hey!</span> Words wrapped in <span style="color:magenta">curly brackets</span> will be <span style="color:magenta">colored!</span>

## <span style="color:#129475">print</span>(<span style="color:purple">string</span> format, <span style="color:purple">char</span> cBracket, <span style="color:purple">params ConsoleColor[]</span> aColors)
- *Note: Number of wrapped words must be equal to number of ConsoleColor elements in specified ConsoleColor array. Otherwise an exception will be thrown*
- Example: <code>print("[Wow!] [Multicolored] [text!]", '[', ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Blue)</code>
<br>Output: <span style="color:red">Wow!</span> <span style="color:green">Multicolored</span> <span style="color:blue">text!</span>

## <span style="color:#129475">println</span>(<span style="color:purple">string</span> format, <span style="color:purple">char</span> cBracket, <span style="color:purple">ConsoleColor</span> fColor)
## <span style="color:#129475">println</span>(<span style="color:purple">string</span> format, <span style="color:purple">char</span> cBracket, <span style="color:purple">ConsoleColor[]</span> aColors)
- Works exactly the same way **print** overrides work. Only difference is that **println** overrides print the line terminator after printing text
