# What it tests

## Versions

So the solution tests all "most current" patch versions since 3.3.8.

All combinations of all versions are tested. 

## Scenarios

All scenarios are run with versions communicating to themselves and each other.

### Send and return

A `Bus.Send(...)` with a `.Register` and a `Bus.Return()`. 

### Encryption

This is included in both the Send Reply and Gateway tests

### Saga autocorrelation

### Send and reply

Effectively a `Bus.Send()` and a `Bus.Reply()`.

### Pub Sub

A `Bus.Publish`.

### Gateway

A `Bus.SendToSites` and a `Bus.Reply`.

### DataBus

 
# TODO

* Messages sent to the error queue and replayed in a diff version