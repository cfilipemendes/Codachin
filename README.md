
# Codachin - A commit viewer

Hi ! First of all thanks for the challenge and it was a great way to improve my knowledge in most of the topics.
So, I tried to do all the points. Between 2.1 and 2.2 I decided to go with the API.

## Software Required.

Git (i.e you should be able to execute git.exe in the shell)

If you want to build and tweak you also need .NET5 support and SDK.

## How to Run

To run my solution you can both Open it in VisualStudio OR Execute the compiled binaries that I've put in the repository aswell.

### 1.1 List Commits

    Open powershell -> cd {RepositoryRoot} -> cd cliExec -> ( Codachin.exe https...repo.git | Codachin.exe --help )

Using **--help** flag it will list more options available for the CLI tool.


### 2.1 HTTP API

    Open powershell -> cd {RepositoryRoot} -> cd apiExec -> Codachin.API.exe 

This will open a terminal window where you should be able to see something like: 

    info: Microsoft.Hosting.Lifetime[0]
      Now listening on: http://localhost:5000


If that happens, just open the link + /swagger -> ex: http://localhost:5000/swagger 

And you should be able to test it right along using the swagger interface.

Another option is using postman or a similar tool to perform a get Request to the endpoint, ex: 

    curl -X GET "http://localhost:5000/log?Url=https://github.com/cfilipemendes/logmytravel.core.git&Page=1&PerPage=14" -H  "accept: */*"


### 3 - Generalization

It's what is implemented at the moment. If a failure happens when retrieving info from Github api then my solution we'll try to use the Git CLI.


Disclaimer, at the moment I've felt that I invested way more time than what I initially supposed that I would invest. 
I do have many things to improve but for me my timeline was this night 2021/07/19 and this is my solution at the moment.

I would like to list a few things that I would improve if I had more time:

* More tests.
* Improve GitCliService , System I.O and Folder accesses can be extracted to another layer/class so we can easily reuse and test them. This would be my next step if tomorrow was not monday :p 
* Improve exception handling. Currently its quite simple and generic but I do have a few ideas to improve it.
* Finally, adding more features ! Changing branches for example. 


I think this is all ! 


Thank you and stay safe,

César Mendes








