# LibIl2Cpp Batch Patcher

Ứng dụng desktop C# để batch patch code vào file libil2cpp.so với giao diện thân thiện.

## Tính năng

- **Batch Patching**: Patch nhiều offset cùng lúc
- **Architecture Support**: Hỗ trợ ARMv7 và ARM64
- **Preset Codes**: 40+ preset codes cho ARMv7, 30+ cho ARM64
- **Custom Hex**: Có thể nhập custom hex code
- **Backup tự động**: Tạo backup trước khi save
- **Giao diện thân thiện**: WinForms interface dễ sử dụng

## Screenshot

![LibIl2Cpp Batch Patcher](screenshot.png)

## Cách sử dụng

1. **Chọn Architecture**: ARMv7 hoặc ARM64
2. **Mở file**: Click "Open libil2cpp.so" và chọn file
3. **Thêm patches**: 
   - Nhập offset (hex) - có thể dùng `1A2B3C` hoặc `0x1A2B3C`
   - Chọn preset code từ dropdown hoặc nhập custom
4. **Quản lý rows**: Add Row / Delete Row
5. **Patch**: Click "Patch All" để patch tất cả rows
6. **Lưu**: Click "Save File"

## Preset Codes

### ARMv7 (40+ codes)
- Value 1/0 (True/False)
- Numbers: 2, 7, 10, 15, 16, 17, 50, 255
- Large numbers: 1K, 10K, 100K, 1M, 10M, 12M
- Float values: 0-2000
- Special: NOP, Speed Hack, Freeze values

### ARM64 (30+ codes)  
- Value 1/0 (True/False)
- Numbers: 2, 7, 10, 15, 16, 17, 50, 100, 255
- Large numbers: 1K-10M, Max Int32
- Float/Double values: 0.0-1000.0
- Instructions: NOP, Return, Branch

## Định dạng Hex

- **Offset**: `1A2B3C` hoặc `0x1A2B3C`
- **Hex Code**: `48 65 6C 6C 6F` hoặc `4865616C6C6F`

## Build

```bash
dotnet build
dotnet run --project LibIl2CppPatcher
```

## Yêu cầu

- .NET 8.0 Windows
- Windows Forms

## Lưu ý

- Luôn tạo backup trước khi patch
- Kiểm tra kỹ offset và bytes trước khi patch  
- File backup sẽ có extension `.backup`
- Hỗ trợ file size lên đến 100MB

## License

MIT License