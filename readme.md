### .NET Full Framework  vs. .NET Core SynchronizationContext

### AspNet Full-framework Api
| framework deadlock/operation | async/await any thread | async/await same thread | async blocking any thread | async blocking same thread |
|------------------------------|------------------------|-------------------------|---------------------------|----------------------------|
| .NET Standard 2.0            |          ❌            |           ❌           |             ❌            |             ✔️             |
| .NET Framework v4.7.2        |          ❌            |           ❌           |             ❌            |             ✔️             |

Testing the Api:
https://localhost:44389/api/values/net-standard/async?configAwait=false - Ok
https://localhost:44389/api/values/net-standard/async?configAwait=true - Ok

https://localhost:44389/api/values/net-standard/sync?configAwait=false - Ok
https://localhost:44389/api/values/net-standard/sync?configAwait=true - Deadlock

https://localhost:44389/api/values/full-framework/async?configAwait=false - Ok
https://localhost:44389/api/values/full-framework/async?configAwait=true - Ok

https://localhost:44389/api/values/full-framework/sync?configAwait=false - Ok
https://localhost:44389/api/values/full-framework/sync?configAwait=true - Deadlock

### AspNetCore Api
| framework deadlock/operation | async/await any thread | async/await same thread | async blocking any thread | async blocking same thread |
|------------------------------|------------------------|-------------------------|---------------------------|----------------------------|
| .NET Standard 2.0            |          ❌            |           ❌           |             ❌            |             ❌            |
| .NET Framework v4.7.2        |          ❌            |           ❌           |             ❌            |             ❌            |

Testing the Api:
dotnet test