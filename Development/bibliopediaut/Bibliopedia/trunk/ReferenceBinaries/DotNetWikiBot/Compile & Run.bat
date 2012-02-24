@rem !! Make sure, that path to C# compiler (csc.exe) is mentioned in Windows PATH variable

@rem !! Uncomment the following line to recompile the "DotNetWikiBot.dll" library itself
@rem csc /t:library DotNetWikiBot.cs

@rem !! Uncomment the following line to recompile the "DotNetWikiBot.dll" library with Microsoft
@rem !! Word interoperability enabled (for Page.ReviseInMSWord() function), and make sure you've
@rem !! installed the proper PIA and the mentioned below path to the PIA is correct
@rem csc /t:library DotNetWikiBot.cs /reference:"C:\Program Files\PIAs for MS Office XP\Microsoft.Office.Interop.Word.dll" /define:MS_WORD_INTEROP

@rem !! Exclude "/debug:full /o-" options on the following line to optimize bot performance
csc BotScript.cs /debug:full /o- /reference:DotNetWikiBot.dll
BotScript.exe
pause