# gitter-api-auth

[![NuGet Status](http://img.shields.io/nuget/v/gitter-api-auth.svg?style=flat)](https://www.nuget.org/packages/gitter-api-auth/)
[![Build status](https://ci.appveyor.com/api/projects/status/tj8e266j1vb193jr?svg=true)](https://ci.appveyor.com/project/Odonno/gitter-api-auth)

*Gitter# Auth* provide you a way to authenticate user on Gitter API. You can use *Gitter# Auth* on these platforms :

* Windows 8.1 (Store Apps)
* Windows Phone 8.1
* Windows 10
* .NET Framework 4.5 [planned]
* Xamarin.Android [planned]
* Xamarin.iOS [planned]


## Authentication process

The authentication is divided in 2 steps that allow you to retrieve a Gitter token. All of these can be found in a single service :

```
public class AuthenticationService : IAuthenticationService
{
    public async Task<bool?> LoginAsync(string oauthKey, string oauthSecret)
    {
        ...
    }

    public async Task<string> RetrieveTokenAsync(...)
    {
        ...
    }
}
```

### Step 1 - Execute login

```
Task<bool?> LoginAsync(string oauthKey, string oauthSecret);
```

To complete...

### Step 2 - Retrieve token

#### Windows 8.1 / Windows 10

```
Task<string> RetrieveTokenAsync()
```

To complete...

#### Windows Phone 8.1

```
Task<string> RetrieveTokenAsync(WebAuthenticationBrokerContinuationEventArgs args)
```

To complete...

