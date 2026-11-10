# Erick-Portfolio — Master Architecture & Specification

## 1. Overview
A modern, ultra-fast portfolio web application built with **Blazor WebAssembly (.NET 10)**, styled with a futuristic **Cyber-Navy & Electric Cyan Glassmorphism** design, and powered directly by **Supabase** (Postgrest, Gotrue, and Storage) via Row-Level Security (RLS) with zero server hosting costs on Cloudflare Pages.

---

## 2. Tech Stack

- **Framework**: Blazor WebAssembly Standalone (`net10.0`)
- **Language**: C# 13+ with Nullable Reference Types enabled
- **Backend / Database**: Supabase PostgreSQL + Auth (Gotrue) + Storage via `supabase-csharp`
- **Security**: PostgreSQL Row-Level Security (RLS) with anonymous public reads and authenticated admin writes
- **CSS / UI**: Tailwind CSS v4 styling rules & custom cyber-navy glass utilities
- **Markdown**: `Markdig` for project case studies
- **Hosting Target**: Cloudflare Pages / Static WASM deployment

---

## 3. Design System & UI Tokens

### Color Palette
| Token | Hex Code | Usage |
|---|---|---|
| `bg-primary` | `#05070B` | Obsidian Black canvas background |
| `bg-surface` | `#0A0F1D` | Deep Navy glass cards (`backdrop-blur-md`) |
| `bg-elevated` | `#111A2E` | Midnight Navy hover states & dropdowns |
| `accent-cyan` | `#00F0FF` | Electric Cyan neon accents, active indicators, borders |
| `accent-blue` | `#2563EB` | Royal Blue secondary gradients |
| `text-primary` | `#F8FAFC` | Slate 50 crisp headings and body copy |
| `text-muted` | `#94A3B8` | Slate 400 descriptions and labels |

### Typography & Effects
- **Heading / Body Font**: `Inter`, system-ui, sans-serif
- **Mono / Code Font**: `JetBrains Mono`, monospace (tags, metrics, status badges)
- **Glassmorphism**: `background: rgba(10, 15, 29, 0.75); backdrop-filter: blur(12px); border: 1px solid rgba(0, 240, 255, 0.15);`
- **Neon Hover**: `box-shadow: 0 0 25px rgba(0, 240, 255, 0.25); border-color: rgba(0, 240, 255, 0.6);`

---

## 4. Architecture & Directory Blueprint

```text
Portfolio.Wasm/
├── .antigravityrules              # Strict instructions + token saver
├── SPECS.md                       # Master architecture & data rules
├── schema.sql                     # Full Supabase DDL + Storage + RLS
├── wwwroot/
│   ├── index.html                 # Cyber-navy terminal preloader + font imports
│   ├── css/app.css                # Custom glassmorphism & neon utilities
│   └── appsettings.json           # Supabase URL & Anon Key
├── Models/                        # Postgrest C# Models inheriting BaseModel
│   └── PortfolioModels.cs         # ProfileSettings, ProjectItem, SkillItem, ExperienceItem, ContactMessage
├── Services/
│   ├── PortfolioDataService.cs    # CRUD & state caching for portfolio items
│   ├── StorageService.cs          # WebP browser conversion & Supabase Storage upload
│   └── SupabaseAuthStateProvider.cs # Custom AuthenticationStateProvider for /admin
├── Layout/
│   ├── MainLayout.razor           # Public layout + Floating glass navbar + footer
│   └── AdminLayout.razor          # Protected admin layout with sidebar
└── Pages/
    ├── Home.razor                 # "/" (Hero, Bento Tech Stack, Projects, Timeline, Contact)
    ├── ProjectDetail.razor        # "/projects/{Slug}" (Markdig case study & gallery)
    ├── Login.razor                # "/login" (Admin authentication)
    └── Admin/
        ├── Dashboard.razor        # "/admin" (Overview metrics)
        ├── ProfileEditor.razor    # "/admin/profile" (Profile & social links editor)
        ├── ProjectsManager.razor  # "/admin/projects" (Project CRUD + Image Uploader)
        ├── SkillsTimeline.razor   # "/admin/experience" (Skills & career history)
        └── Inbox.razor            # "/admin/inbox" (Visitor inquiries)
```

---

## 5. Sprint Execution Plan

### Sprint 1: Setup, Architecture & Data Service
- [x] Create project solution and Blazor WebAssembly .NET 10 app.
- [x] Install NuGet packages: `supabase-csharp`, `Microsoft.AspNetCore.Components.Authorization`, `Markdig`.
- [x] Create Postgrest C# models with `[Table]` and `[Column]` mappings.
- [x] Implement `StorageService` with in-browser WebP image optimization.
- [x] Implement `PortfolioDataService` with fallback demo data (allowing local run without Supabase credentials).
- [x] Configure dependency injection in `Program.cs`.

### Sprint 2: Dark Cyber-Navy Public Shell & Hero
- [x] Build futuristic cyber-navy WASM preloader in `index.html` with terminal styling and dynamic progress bar.
- [x] Build `MainLayout.razor` with floating glassmorphism navbar, live status indicator, and footer.
- [x] Build `Home.razor` Hero Section with dynamic profile binding, Bento Tech Stack grid, and Experience Timeline.

### Sprint 3: Projects Showcase, Markdown Case Studies & Contact Form
- [x] Implement filterable Projects Grid on `Home.razor` with category pills and neon glow cards.
- [x] Implement `ProjectDetail.razor` (`/projects/{Slug}`) with `Markdig` markdown parsing and screenshot gallery.
- [x] Implement working Contact Form on `Home.razor` saving messages directly to Supabase with instant feedback.

### Sprint 4 & 5: Authentication, Admin Dashboard & Storage Management
- [x] Implement `SupabaseAuthStateProvider` bridging Supabase Gotrue sessions to Blazor `<AuthorizeView>`.
- [x] Build `/login` page with cyber-navy glass styling and error handling.
- [x] Build `AdminLayout.razor` and `/admin` Dashboard with real-time stats.
- [x] Build `/admin/profile`, `/admin/projects` (with drag-and-drop WebP image uploader), `/admin/experience`, and `/admin/inbox`.
