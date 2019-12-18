### .NET Full Framework  vs. .NET Core SynchronizationContext

### AspNet Full-framework Api
| framework deadlock/operation | async/await any thread | async/await same thread | async blocking any thread | async blocking same thread |
|------------------------------|------------------------|-------------------------|---------------------------|----------------------------|
| .NET Standard 2.0            |          ❌            |           ❌           |             ❌            |             ✔️             |
| .NET Framework v4.7.2        |          ❌            |           ❌           |             ❌            |             ✔️             |

### AspNetCore Api
| framework deadlock/operation | async/await any thread | async/await same thread | async blocking any thread | async blocking same thread |
|------------------------------|------------------------|-------------------------|---------------------------|----------------------------|
| .NET Standard 2.0            |          ❌            |           ❌           |             ❌            |             ❌            |
| .NET Framework v4.7.2        |          ❌            |           ❌           |             ❌            |             ❌            |