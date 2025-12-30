# LibIl2Cpp Batch Patcher

A C# desktop application for batch patching code into `libil2cpp.so` with a user-friendly interface.

## Features
- Batch patch multiple offsets at once
- Supports ARMv7 and ARM64
- 40+ preset codes for ARMv7, 30+ for ARM64
- Custom hex input
- Automatic backup before saving
- Easy-to-use WinForms UI

## Screenshot
<img width="777" height="584" alt="image" src="https://github.com/user-attachments/assets/246e23da-ef7a-4b44-ba4f-28d9484d6de0" />


## Usage
1. Select architecture (ARMv7 or ARM64)
2. Open `libil2cpp.so`
3. Add patches:
   - Enter offset in hex (`1A2B3C` or `0x1A2B3C`)
   - Select a preset code or enter custom hex
4. Manage rows (Add / Delete)
5. Click **Patch All**
6. Click **Save File**

## Preset Codes

### ARMv7
- Boolean values (1 / 0)
- Common numbers: 2, 7, 10, 15, 16, 17, 50, 255
- Large values: 1K, 10K, 100K, 1M, 10M, 12M
- Float values (0–2000)
- Special: NOP, Speed Hack, Freeze values

### ARM64
- Boolean values (1 / 0)
- Common numbers: 2, 7, 10, 15, 16, 17, 50, 100, 255
- Large values: 1K–10M, Max Int32
- Float / Double values (0.0–1000.0)
- Instructions: NOP, Return, Branch

## Hex Format
- **Offset**: `1A2B3C` or `0x1A2B3C`
- **Hex Code**: `48 65 6C 6F` or `48656C6F`

## Build
```bash
dotnet build
dotnet run --project LibIl2CppPatcher
