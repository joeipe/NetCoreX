# 🚀 NetCoreX

**NetCoreX** is a modern .NET API playground designed to showcase the latest features of the .NET ecosystem — including **Minimal APIs**, **JWT authentication**, and up-to-date .NET runtime capabilities.

This repository is ideal for:

- Experimenting with new .NET versions (e.g. **.NET 9 / .NET 10 previews**)
- Demonstrating **best-practice API patterns**
- Serving as a **reference or starter template** for real-world APIs

---

## ✨ Key Focus Areas

- Minimal API architecture
- JWT authentication & authorization
- Modern .NET configuration patterns
- Clean, extensible project structure
- Forward-compatible with future .NET releases

---

## 🔐 JWT Development Tokens (Reference)

For **local development**, this project uses the built-in `dotnet user-jwts` tooling to generate and manage JWTs.

### View available options
```bash
dotnet user-jwts create --help
dotnet user-jwts create --audience ncx-api
dotnet user-jwts create --audience ncx-api --claim country=australia --role admin
dotnet user-jwts print 2c1d30c7