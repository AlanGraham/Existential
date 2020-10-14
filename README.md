# About Existential&#46;NET
<img alt="Azure DevOps builds (branch)" src="https://img.shields.io/azure-devops/build/ggreig/Existential/1/main">
<img alt="SonarCloud quality gate" src="https://sonarcloud.io/api/project_badges/measure?project=ggreig_Existential&metric=alert_status">
<img alt="Azure DevOps releases" src="https://img.shields.io/azure-devops/release/ggreig/9c4fc971-bef3-428a-ab81-cf30a24bea74/1/1">
<a href="https://www.nuget.org/packages/Existential.Net"><img alt="Nuget (with prereleases)" src="https://img.shields.io/nuget/vpre/Existential.Net"></a>

Existential is a utility library, published as a NuGet package,
for dealing with issues such as whether a value exists or not, 
or whether it exists in the desired form. It contains validation methods,
a Maybe monad, and more.

# Getting Started
Install the [NuGet package](https://www.nuget.org/packages/Existential.Net) into any library where you want to use it, and read the 
[user/API documentation](https://existential.ggreig.com/)
for what you can do with it. It has no other dependencies.

# Build and Test
You can build this library using Visual Studio 2019. So long as you have an SDK installed
that can target .NET Standard 2.0 you can build the library itself. The documentation
and test projects also require support for .NET Core 3.1.

# Contribute
Fork the code to your own account in Azure DevOps and submit a pull request. A lot of open-source code
is on GitHub. Sorry, this isn't, because I also use Azure DevOps professionally, need a testing ground,
and this is it. If you don't want to sign up to Azure DevOps yourself (it's 
[free for up to 5 people](https://azure.microsoft.com/en-us/pricing/details/devops/azure-devops-services/)), 
you can submit any bugs or feature 
requests through the "Contact owners" form 
[on NuGet.org](https://www.nuget.org/packages/Existential.Net). I may look into mirroring to GitHub, but
it's not a top priority.