# Geonames.Parser.Contract

Contract library for Geonames.Parser containing abstractions, data models, and enums for Geonames.org datasets.
Provides the interfaces and models needed to implement custom data processors and interact with the Geonames parser.

## Installation

```bash
dotnet add package R83.Geonames.Parser.Contract
```

## Usage

Use this package to reference the Geonames data models and `IGeonamesParser` interface without taking a dependency on the full implementation.
Ideal for shared libraries or projects that only need to define data processing logic.