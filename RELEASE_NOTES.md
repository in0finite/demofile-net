### 0.4.1 (2023-12-09)

- Update to schema v13980
- Improve fallback field decoding (GameTime_t, GameTick_t and heuristics for enums)

### 0.3.6 (2023-11-25)

- Fix alternate baseline regression introduced in 0.3.x

### 0.3.5 (2023-11-25)

- Added release notes

### 0.3.1 (2023-11-25)

- Fix StrictEHandle event key parsing (fixes `FooPawn` properties on game events)

### 0.2.9 (2023-11-12)

- Use lower byte of user ID to get controller slot in game events

### 0.2.8 (2023-11-12)

- Fix stringtable updates sometimes being off by one

### 0.2.7 (2023-11-12)

- Fix userid event vars being parsed as entity indexes
- Add GetPlayerByUserId/SteamId helper methods

### 0.2.6 (2023-11-11)

- Add autogen helpers for entity handle schema fields

### 0.2.5 (2023-11-11)

- Add support for MNetworkVarTypeOverride
- Add class-level metadata as comments

### 0.2.4 (2023-11-08)

- Add GameItems class
- Add name/paintkit/rarity/quality helper properties to CEconItemView

### 0.2.3 (2023-11-05)

- Update schema for v13966
- Add CCSPlayerPawn.InputButtons and CSMovementServices

### 0.2.2 (2023-10-29)

- Add events for entity creation/update/deletion
- Improve game event generation

### 0.2.1 (2023-10-25)

- Update schema for v13964
- Support fallback field decoding
