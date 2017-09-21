# Contributing to the Serilog Console Sink

First of all thanks for dropping by, feel free to checkout [Serilog core project's contributing page](https://github.com/serilog/serilog/blob/dev/CONTRIBUTING.md) which contains some key points about the organisation.

## Reporting an issue

Bugs are tracked via [GitHub][issue_list] issues.  Below are some notes to help create a new issue.  The issue template will help you on the way.

* Create an issue via the [issues list][create_issue].
* List the version of Serilog that is affected
* List the target framework and operating system
* If possible, provide a sample that reproduces the issue.

## Requesting a feature/enhancement

Feature as also tracked via [GitHub][issue_list] issues.  Below are some notes to help create an issue.  The issue template will help you on the way

* Create an issue via the [issues list][create_issue].
* List the version of Serilog that is affected
* List the target framework and operating system
* If possible, provide a sample that reproduces the issue.

## Making a PR

* If an issue does not already exist please create one via the issues list.
* Fork the repository and create a branch with a descriptive name.
* Attempt to make commits of logical units.
* When committing, please reference the issue the commit relates to.
* Run the build and tests.
    * Windows platforms can use the `build.ps1` script. (This is the script used in AppVeyor builds)
    * nix/OSX platforms can use the `build.sh` script.  (This is the script used in Travis builds)
* Create the PR, the PR template will help provide a stub of what information is required including:
    * The issue this PR addresses
    * Unit Tests for the changes have been added.

## Questions?

Serilog has an active and helpful community who are happy to help point you in the right direction or work through any issues you might encounter. You can get in touch via:

 * [Stack Overflow](http://stackoverflow.com/questions/tagged/serilog) - this is the best place to start if you have a question
 * Our [issue tracker](https://github.com/serilog/serilog/issues) here on GitHub
 * [Gitter chat](https://gitter.im/serilog/serilog)
 * The [#serilog tag on Twitter](https://twitter.com/search?q=%23serilog)

Finally when contributing please keep in mind our [Code of Conduct][serilog_code_of_conduct].

[serilog]: https://github.com/serilog/serilog
[sinks]: https://github.com/serilog/serilog/wiki/Provided-Sinks
[community_projects]: https://github.com/serilog/serilog/wiki/Community-Projects
[create_issue]: https://github.com/serilog/serilog-sinks-console/issues/new
[issue_list]: https://github.com/serilog/serilog-sinks-console/issues/
[serilog_code_of_conduct]: https://github.com/serilog/serilog/blob/dev/CODE_OF_CONDUCT.md
