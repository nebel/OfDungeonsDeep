# DeeperDeepDungeonDex

Rewrite of DeepDungeonDex.

## Why?

Spite.

## Updating data

To update mob/floor information:

```sh
dotnet run --project DeeperDeepDungeonDex.MarkdownMania -- "F:\games\standalone\xiv\game\sqpack"
```

## TODO

- [x] Parse the already existing Deep Dungeon spreadsheets instead of the YAML submodule bullshit
  - This information is wrong and buggy - best to use the existing data the community's made
- [x] Show basic mob information
- [ ] Show basic floor information
- [ ] Show The Strat:tm:
