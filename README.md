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

The authentication is divided in 2 steps that allow you to retrieve a Gitter token. These 2 steps can be found in a single service :

```
public class AuthenticationService
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

On Windows 8.1 / 10 and Windows Phone 8.1, the login process use a service called *WebAuthenticationBroker* through OAuth2. This method requires your **OauthKey** and your **OauthSecret**. You can retrieve these parameters from the developer website : [https://developer.gitter.im/docs/authentication](https://developer.gitter.im/docs/authentication).

### Step 2 - Retrieve token

#### Windows 8.1 / Windows 10

```
Task<string> RetrieveTokenAsync();
```

On Windows 8.1 / 10, once you're authenticated (cf. step 1), you only have to retrieve the provided token as it is.

#### Windows Phone 8.1

```
Task<string> RetrieveTokenAsync(WebAuthenticationBrokerContinuationEventArgs args);
```

On Windows Phone 8.1, it is a little different, you have to pass a parameter : **WebAuthenticationBrokerContinuationEventArgs**. This event result comes from the continuation service of the *WebAuthenticationBroker*.

Here is an example of how to implement OAuth2 authentication using *WebAuthenticationBroker* on Universal Apps (W8.1 / WP8.1) : [https://code.msdn.microsoft.com/windowsapps/Authentication-using-bb28840e#content](https://code.msdn.microsoft.com/windowsapps/Authentication-using-bb28840e#content).
