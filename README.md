# Martkeeper

## Description

TODO

## Prerequisites
* At least Godot 4.6.1 (.NET Edition)
* At least .NET SDK 8.0+

## Development Setup
This project uses a T4 template to generate constants. 
Before your first build, run:


```bash
dotnet tool restore
```

Before building the project (hammer icon in godot editor) T4 tool will be run and the translations static class TR will be updated.

## Project

### Translations

Add translations in localization/translations.csv
When building the project the static class TRKeys will be updated automatically, use it for in game texts.

Translated texts are used in the following fashion: `TR.KEY_NAME`, where KEY_NAME is one of the entries in the csv file.

## License & Copyright

Copyright © 2026 Lui & Jean Rocha