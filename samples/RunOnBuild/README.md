# Run on Build

Add `Exec` task to `.csproj` file.

```xml
<Target Name="CSharpToTypeScript" BeforeTargets="Build">
    <Exec Command="dotnet cs2ts ./DTOs -o ./Client/models -i Simple -q Single -c" />
</Target>
```