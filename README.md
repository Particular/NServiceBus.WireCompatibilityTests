# NServiceBus.WireCompatibilityTests

Tests to ensure wire compatibility between versions of NServiceBus. The tests use MSMQ as the underlying transport since its been available since the start


## Versions/NuGets

So the solution tests all "most current" patch versions since 3.3.8.

All NuGets are specified with a wildcard for the patch.

```
<PackageReference Include="NServiceBus" Version="3.3.*" />
```

The Callbacks and Encryption solutions, when targeting the external packages, the project targets the current minor of the core. e.g.

```
<PackageReference Include="NServiceBus" Version="6.*" />
<PackageReference Include="NServiceBus.Callbacks" Version="1.1.*" />
```


## Solutions


### Core

All scenarios are run with versions communicating to themselves and each other.


#### Saga autocorrelation


#### Send and reply

Effectively a `Bus.Send()` and a `Bus.Reply()`.


#### Pub Sub

A `Bus.Publish`.


#### DataBus


### Encryption

This is included in both the Send Reply and Gateway tests


### Callbacks

A `Bus.Send(...)` with a `.Register` and a `Bus.Return()`.