# Skyward Ascent: Design Plan

> Base context document. Paste this at the start of a new prompt when working on the game.

## 1. Project snapshot

- **Game:** Skyward Ascent (working title)
- **Genre:** 2D auto-run platformer with biome-based progression
- **Engine:** Unity 6 (6000.1.12f1), Universal 2D template, URP 2D Renderer
- **Platform:** Windows (editor and standalone target)
- **Developer level:** beginner in Unity, some prior coding experience
- **Version control:** Git + GitHub, Unity `.gitignore`
- **Input:** new Input System package installed; Active Input Handling set to "Both"
- **Unity 6 notes:** use `Rigidbody2D.linearVelocity` (not `velocity`); solutions should be Unity 6-specific

## 2. Concept

The character is always running forward on its own. The world trends upward, so the journey is a climb through biomes toward higher altitudes. The player controls only a few simple actions, ideally one button or tap. Each new biome adds something new to the gameplay.

Inspiration is the calm, flowing feel of Alto's Odyssey (constant running, one-touch controls). The mechanics and progression are the game's own.

## 3. Core loop

1. The character auto-runs forward at a constant (later increasing) speed.
2. Terrain chunks spawn ahead and slope or step upward overall.
3. The player uses a small set of actions (jump first) to cross gaps and avoid hazards.
4. Altitude and distance increase until a biome transition is reached.
5. A short slow-motion moment marks the transition, and the new biome adds one new mechanic (ability or hazard).
6. Earlier mechanics stay active, so difficulty builds naturally.
7. Failing (falling into a pit, hitting a hazard) ends the run.

## 4. Biome progression (draft)

Biome order: forest, storm, clouds, ruins, space. The "new addition" column is a first draft.

| Biome | New addition (draft) | Notes |
|---|---|---|
| Forest | Jump | Tutorial biome: gaps, logs, simple timing |
| Storm | Wind | Gusts push the player; possible hold-to-brace action |
| Clouds | Glide or double jump | Soft cloud platforms, longer airtime |
| Ruins | Slide or dash | Crumbling floors, low gaps |
| Space | Low gravity or gravity flip | Floaty jumps, alternate paths |

## 5. Controls

- Goal: minimum actions, ideally one button or tap.
- Jump is the base action. Each biome can add one more action, mapped to the same input (hold, double tap, etc.) where possible.
- Final mapping is decided as mechanics are added.

## 6. Technical plan

- **Player:** constant forward `linearVelocity.x`; jump on input; ability flags that biomes switch on. Rigidbody2D Z rotation is frozen. A `GroundCheck` child uses `Physics2D.OverlapCircle` on the `Ground` layer.
- **Camera:** follow script in `LateUpdate()` that tracks forward (x) movement and upward y as needed, and never scrolls backward.
- **Terrain:** chunk-based spawner. Each chunk is a prefab with a defined start and end point so chunks connect cleanly. Spawn ahead of the camera and delete chunks far behind it. Keep the maximum gap and height difference within jump reach.
- **Biomes:** a `BiomeData` ScriptableObject per biome holding the altitude or distance threshold, chunk prefab list, palette and background, and enabled abilities and hazards. The spawner reads the current biome and uses a transition chunk between biomes.
- **Transitions:** slow-motion cutscene at the end of each biome that presents the new mechanic.
- **Layers and sorting:** `Ground` layer exists. Add sorting layers (Background, Platforms, Player, FX) when art goes in.

## 7. Art direction (details open)

- A mix of self-made art and possibly free asset packs, kept stylistically consistent.
- Suggested approach: low-res pixel art or flat style, limited palette per biome, placeholders until gameplay feels good.
- Minimum art set first: player (idle, run, jump), one ground tile type, one forest background.

## 8. Milestones

1. **M1: Core run.** Auto-run, jump, ground check, camera follow, and a chunk spawner producing a simple forest biome. Tune until it feels good.
2. **M2: Biome system.** `BiomeData` ScriptableObject, a second biome with one new mechanic, and a transition chunk. Proves the system.
3. **M3: Remaining biomes.** Add storm, clouds, ruins, and space one at a time.
4. **M4: Polish.** Altitude and distance UI, game over and restart, slow-motion transitions, art pass, audio.

## 9. Existing project state

- Player movement uses `Rigidbody2D.linearVelocity`.
- Ground detection with `GroundCheck` and the `Ground` layer works.
- A camera follow script exists.
- A platform spawner exists using relative offsets and configurable spacing, usable as a starting point for chunk spawning.
- Folder structure: Scripts, Prefabs, Sprites, Scenes.

## 10. Open questions

- Are abilities permanent unlocks, or do they exist only inside their biome?
- Does the title stay, given the sideways-dominant travel?
- Should there be a defined ending (reaching space) or an endless mode after it?
- Final one-button input mapping for each added action.

## 11. How to use this document in prompts

Paste this file and add the specific task, for example: "Using the plan above, help me write the chunk spawner for Milestone 1." Keep the file updated whenever a decision is made, and commit the update with the code.
