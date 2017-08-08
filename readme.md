Unit Testing Razor Views
========================

Simple example showing how to use RazorGenerator and the HTML agility pack to unit test MVC razor views.

So, what problem are we trying to solve with unit testing views?  In a recent agile project delivering an MVC website, we adopted the standard agile testing triangle, with many unit tests, a lower number of integration tests, and fewer still UI tests (using Selenium and Seleno).  The reason for having fewest UI tests, is the same issue everyone faces...  They are slow to run (and hence generally only get run on nightly build, so very slow feedback), they are brittle and not reslient to change (to hard to maintain) and when they fail it can be hard to locate the "root cause".  

However, we found in practice, we were writing many more Selenium UI tests than we wanted, to test UI specific behaviour.  Take paging for example, there are a whole bunch of scenarios about when various page links should appear in the rendered view (first page how have no previous, last page should have no next, middle page should have prev and next etc.).  Using Selenium for testing all that logic seemed overkill, and we were in danger of creating a bit of a test framework millstone, which we would need to carry throughout the life of the website. That said, we do want to have confidence (in the way of authomated tests) that the paging logic is working correctly.

Enter unit testing the views themseleves (well the views and their associated view models).  With this approach, we wanted unit style tests which would run very quickly and provide fast feedback.  This allows us to write less UI tests with Selenium, hence allowing us to reseve the UI testing for "feature level, happy case tests" (leading to a manageable number of UI tests).

We tried a number of approaches to the unit test views problem.  We started with the various libraries and code samples available which use a Fake Controller, and allow you to execute a razor view and return a Html string.  However, we had issues here, in that these worked fine when run from an ASP.NET project (where we had HttpContext, ControllerContext etc.), but did not work well when run in a DLL test project (the internal MVC framework complaining about missing core objects context related objects, despite our best efforts to mock these dependencies).  I am sure it is possible to get this working, but it seemed too hard to us, and we felt we were having to do unreasobale things to make this approach to unit testing work, and be easily supportable.  This lead us to seek another approach...

In the end we settled on using two core extensions / libraries...
* RazorGenerator VS extension (VS 2017 at time of writing).
* Html Agility Pack

The RazorGenerator extension is used in the main web project, and it automatically detects changes to a cshtml file, and generates a .cs file representing the view.  This is what we use in our tests, it is the compiled version of the view.  The HtmlAgility pack is used only within the test project.  It allows us to take the Html String output from the auto generated view class, and load it into the equivelant of a browser DOM.  We can then use this in out tests to check if certain aspects of the UI have been rendered within the view.  

For example, if we want to test paging, we create a view model with sufficient items to cause paging, we execute the auto generated view class using the view model, and then check for the presence (or absence) of paging UI elements using the Agility Pack.

Disclaimer... this is not supposed to be a best practice inmplementation, only a simple implementation to give those new to unit testing razor views an idea of how it works with a simple example.  Futhermore, whilst we are happy this approach works on simple razor views using @html helpers, we may find that certain more complex views cause isses... but we have not had this so far.

Enjoy!

