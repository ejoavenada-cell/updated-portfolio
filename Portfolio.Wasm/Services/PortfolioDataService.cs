using Portfolio.Wasm.Models;

namespace Portfolio.Wasm.Services;

public class PortfolioDataService
{
    private readonly Supabase.Client? _supabase;

    // In-memory cache for ultra-fast instant 0ms navigation
    private ProfileSettings? _cachedProfile;
    private List<ProjectItem>? _cachedProjects;
    private List<SkillItem>? _cachedSkills;
    private List<ExperienceItem>? _cachedExperiences;
    private List<ContactMessage>? _cachedMessages;

    public PortfolioDataService(Supabase.Client? supabase = null)
    {
        _supabase = supabase;
        InitializeMockData();
    }

    private void InitializeMockData()
    {
        _cachedProfile = new ProfileSettings
        {
            FullName = "Erick John",
            Headline = ".NET & Full-Stack Software Developer",
            StatusText = "Available for Opportunities",
            Bio = "Specializing in C# 13, .NET 10 Blazor WebAssembly, cloud-native architectures, and modern high-performance web applications with clean architecture and reactive UI.",
            EmailAddress = "erickjohn.dev@example.com",
            GithubUrl = "https://github.com",
            LinkedinUrl = "https://linkedin.com",
            ResumeUrl = "#",
            AvatarUrl = "https://images.unsplash.com/photo-1534528741775-53994a69daeb?auto=format&fit=crop&w=400&q=80"
        };

        _cachedProjects = new List<ProjectItem>
        {
            new()
            {
                Title = "CloudScale Observability Platform",
                Slug = "cloudscale-observability",
                Summary = "High-throughput telemetry ingestion and real-time dashboard engine built with .NET 10, Blazor WASM, and TimescaleDB.",
                ContentMarkdown = "### Architecture Overview\n\nCloudScale delivers sub-second telemetry aggregation across Kubernetes clusters.\n\n- **Backend**: C# .NET 10 microservices with gRPC streaming\n- **Frontend**: Blazor WebAssembly with custom Canvas / WebGL metrics visualizer\n- **Database**: PostgreSQL with TimescaleDB hypertable extensions\n- **Zero Cold-Starts**: Compiled ahead-of-time with static asset compression",
                ThumbnailUrl = "https://images.unsplash.com/photo-1551288049-bebda4e38f71?auto=format&fit=crop&w=800&q=80",
                GalleryUrls = new()
                {
                    "https://images.unsplash.com/photo-1551288049-bebda4e38f71?auto=format&fit=crop&w=1200&q=80",
                    "https://images.unsplash.com/photo-1504639725590-34d0984388bd?auto=format&fit=crop&w=1200&q=80"
                },
                TechTags = new() { ".NET 10", "Blazor WASM", "PostgreSQL", "Tailwind CSS", "Docker" },
                LiveUrl = "https://example.com/demo",
                RepoUrl = "https://github.com/example/cloudscale",
                IsFeatured = true,
                DisplayOrder = 1
            },
            new()
            {
                Title = "Aura Cyber Commerce Engine",
                Slug = "aura-cyber-commerce",
                Summary = "Headless e-commerce platform featuring instantaneous search, optimistic UI updates, and serverless payment webhooks.",
                ContentMarkdown = "### Project Case Study\n\nAura was built to demonstrate extreme front-end responsiveness using Blazor WASM and Supabase Row-Level Security.\n\n- **Inventory Synchronization**: Real-time broadcast using Supabase Realtime channels\n- **Security**: PostgreSQL Row-Level Security preventing unauthorized catalog edits\n- **Design**: Cyber-Navy glassmorphic UI with zero layout shifts",
                ThumbnailUrl = "https://images.unsplash.com/photo-1526374965328-7f61d4dc18c5?auto=format&fit=crop&w=800&q=80",
                GalleryUrls = new()
                {
                    "https://images.unsplash.com/photo-1526374965328-7f61d4dc18c5?auto=format&fit=crop&w=1200&q=80"
                },
                TechTags = new() { "C#", "Blazor", "Supabase", "Stripe API", "Tailwind CSS" },
                LiveUrl = "https://example.com/aura",
                RepoUrl = "https://github.com/example/aura-commerce",
                IsFeatured = true,
                DisplayOrder = 2
            },
            new()
            {
                Title = "DevPulse AI Assistant Hub",
                Slug = "devpulse-ai-hub",
                Summary = "Interactive developer dashboard integrating LLM workflows, automated code review pipelines, and Git commit insights.",
                ContentMarkdown = "### Overview\n\nDevPulse bridges developer workflow automation with direct AI evaluation.\n\n- Multi-provider LLM token streaming into Blazor WASM UI\n- Secure API key storage and audit logging\n- Rich markdown documentation generation via Markdig",
                ThumbnailUrl = "https://images.unsplash.com/photo-1618005182384-a83a8bd57fbe?auto=format&fit=crop&w=800&q=80",
                GalleryUrls = new()
                {
                    "https://images.unsplash.com/photo-1618005182384-a83a8bd57fbe?auto=format&fit=crop&w=1200&q=80"
                },
                TechTags = new() { ".NET 10", "AI/LLM", "C# 13", "WebAssembly", "Cloudflare" },
                LiveUrl = "https://example.com/devpulse",
                RepoUrl = "https://github.com/example/devpulse",
                IsFeatured = true,
                DisplayOrder = 3
            }
        };

        _cachedSkills = new List<SkillItem>
        {
            new() { Name = "C# / .NET 10", Category = "Backend", DisplayOrder = 1, IconName = "code" },
            new() { Name = "ASP.NET Core & Web API", Category = "Backend", DisplayOrder = 2, IconName = "server" },
            new() { Name = "Entity Framework / Postgrest", Category = "Backend", DisplayOrder = 3, IconName = "database" },
            new() { Name = "PostgreSQL & Supabase RLS", Category = "Backend", DisplayOrder = 4, IconName = "shield-check" },
            new() { Name = "Blazor WebAssembly (WASM)", Category = "Frontend", DisplayOrder = 5, IconName = "layers" },
            new() { Name = "Tailwind CSS & Glassmorphism", Category = "Frontend", DisplayOrder = 6, IconName = "palette" },
            new() { Name = "HTML5 / Modern JS / WASM Interop", Category = "Frontend", DisplayOrder = 7, IconName = "globe" },
            new() { Name = "Docker & Microservices", Category = "Tools", DisplayOrder = 8, IconName = "box" },
            new() { Name = "Git & CI/CD Pipelines", Category = "Tools", DisplayOrder = 9, IconName = "git-branch" },
            new() { Name = "Cloudflare Pages & Edge Hosting", Category = "Tools", DisplayOrder = 10, IconName = "cloud" }
        };

        _cachedExperiences = new List<ExperienceItem>
        {
            new()
            {
                RoleOrDegree = "Senior Full-Stack .NET Developer",
                Organization = "Apex Digital Systems",
                Period = "2023 - Present",
                Description = "Architected high-scale enterprise Blazor WASM applications, optimized .NET backend microservices, and migrated relational databases to Postgres with RLS security policies.",
                Type = "Work",
                DisplayOrder = 1
            },
            new()
            {
                RoleOrDegree = "Software Engineer (.NET / Web)",
                Organization = "Nova Innovations",
                Period = "2021 - 2023",
                Description = "Built responsive Single Page Applications in Blazor and React, integrated REST & GraphQL APIs, and automated cloud deployments with zero-downtime CI/CD workflows.",
                Type = "Work",
                DisplayOrder = 2
            },
            new()
            {
                RoleOrDegree = "B.S. in Computer Science / Information Technology",
                Organization = "University Institute of Technology",
                Period = "2017 - 2021",
                Description = "Focused on Object-Oriented Software Design, Database Systems, Computer Networks, and Distributed Architecture.",
                Type = "Education",
                DisplayOrder = 3
            }
        };

        _cachedMessages = new List<ContactMessage>
        {
            new()
            {
                SenderName = "Sarah Jenkins",
                SenderEmail = "sarah.j@techrecruiter.io",
                Message = "Hi Erick, I came across your .NET 10 Blazor portfolio. We have an exciting Senior .NET / Cloud role that matches your skills perfectly!",
                IsRead = false,
                CreatedAt = DateTime.UtcNow.AddHours(-3)
            }
        };
    }

    // ==========================================
    // PROFILE
    // ==========================================
    public async Task<ProfileSettings> GetProfileAsync(bool forceReload = false)
    {
        if (_cachedProfile != null && !forceReload)
            return _cachedProfile;

        if (_supabase != null)
        {
            try
            {
                var response = await _supabase.From<ProfileSettings>().Single();
                if (response != null)
                {
                    _cachedProfile = response;
                    return _cachedProfile;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Supabase GetProfile error: {ex.Message}");
            }
        }

        return _cachedProfile ??= new ProfileSettings();
    }

    public async Task SaveProfileAsync(ProfileSettings profile)
    {
        profile.UpdatedAt = DateTime.UtcNow;
        _cachedProfile = profile;

        if (_supabase != null)
        {
            try
            {
                await _supabase.From<ProfileSettings>().Upsert(profile);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Supabase SaveProfile error: {ex.Message}");
            }
        }
    }

    // ==========================================
    // PROJECTS
    // ==========================================
    public async Task<List<ProjectItem>> GetProjectsAsync(bool forceReload = false)
    {
        if (_cachedProjects != null && !forceReload)
            return _cachedProjects;

        if (_supabase != null)
        {
            try
            {
                var response = await _supabase.From<ProjectItem>()
                    .Order(x => x.DisplayOrder, Supabase.Postgrest.Constants.Ordering.Ascending)
                    .Get();

                if (response?.Models?.Count > 0)
                {
                    _cachedProjects = response.Models;
                    return _cachedProjects;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Supabase GetProjects error: {ex.Message}");
            }
        }

        return _cachedProjects ??= new();
    }

    public async Task<ProjectItem?> GetProjectBySlugAsync(string slug)
    {
        var projects = await GetProjectsAsync();
        return projects.FirstOrDefault(p => string.Equals(p.Slug, slug, StringComparison.OrdinalIgnoreCase));
    }

    public async Task SaveProjectAsync(ProjectItem project)
    {
        if (_cachedProjects == null)
            await GetProjectsAsync();

        var index = _cachedProjects!.FindIndex(p => p.Id == project.Id);
        if (index >= 0)
            _cachedProjects[index] = project;
        else
            _cachedProjects.Add(project);

        if (_supabase != null)
        {
            try
            {
                await _supabase.From<ProjectItem>().Upsert(project);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Supabase SaveProject error: {ex.Message}");
            }
        }
    }

    public async Task DeleteProjectAsync(Guid id)
    {
        _cachedProjects?.RemoveAll(p => p.Id == id);

        if (_supabase != null)
        {
            try
            {
                await _supabase.From<ProjectItem>().Where(x => x.Id == id).Delete();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Supabase DeleteProject error: {ex.Message}");
            }
        }
    }

    // ==========================================
    // SKILLS
    // ==========================================
    public async Task<List<SkillItem>> GetSkillsAsync(bool forceReload = false)
    {
        if (_cachedSkills != null && !forceReload)
            return _cachedSkills;

        if (_supabase != null)
        {
            try
            {
                var response = await _supabase.From<SkillItem>()
                    .Order(x => x.DisplayOrder, Supabase.Postgrest.Constants.Ordering.Ascending)
                    .Get();

                if (response?.Models?.Count > 0)
                {
                    _cachedSkills = response.Models;
                    return _cachedSkills;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Supabase GetSkills error: {ex.Message}");
            }
        }

        return _cachedSkills ??= new();
    }

    public async Task SaveSkillAsync(SkillItem skill)
    {
        if (_cachedSkills == null)
            await GetSkillsAsync();

        var index = _cachedSkills!.FindIndex(s => s.Id == skill.Id);
        if (index >= 0)
            _cachedSkills[index] = skill;
        else
            _cachedSkills.Add(skill);

        if (_supabase != null)
        {
            try
            {
                await _supabase.From<SkillItem>().Upsert(skill);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Supabase SaveSkill error: {ex.Message}");
            }
        }
    }

    public async Task DeleteSkillAsync(Guid id)
    {
        _cachedSkills?.RemoveAll(s => s.Id == id);

        if (_supabase != null)
        {
            try
            {
                await _supabase.From<SkillItem>().Where(x => x.Id == id).Delete();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Supabase DeleteSkill error: {ex.Message}");
            }
        }
    }

    // ==========================================
    // EXPERIENCES
    // ==========================================
    public async Task<List<ExperienceItem>> GetExperiencesAsync(bool forceReload = false)
    {
        if (_cachedExperiences != null && !forceReload)
            return _cachedExperiences;

        if (_supabase != null)
        {
            try
            {
                var response = await _supabase.From<ExperienceItem>()
                    .Order(x => x.DisplayOrder, Supabase.Postgrest.Constants.Ordering.Ascending)
                    .Get();

                if (response?.Models?.Count > 0)
                {
                    _cachedExperiences = response.Models;
                    return _cachedExperiences;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Supabase GetExperiences error: {ex.Message}");
            }
        }

        return _cachedExperiences ??= new();
    }

    public async Task SaveExperienceAsync(ExperienceItem experience)
    {
        if (_cachedExperiences == null)
            await GetExperiencesAsync();

        var index = _cachedExperiences!.FindIndex(e => e.Id == experience.Id);
        if (index >= 0)
            _cachedExperiences[index] = experience;
        else
            _cachedExperiences.Add(experience);

        if (_supabase != null)
        {
            try
            {
                await _supabase.From<ExperienceItem>().Upsert(experience);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Supabase SaveExperience error: {ex.Message}");
            }
        }
    }

    public async Task DeleteExperienceAsync(Guid id)
    {
        _cachedExperiences?.RemoveAll(e => e.Id == id);

        if (_supabase != null)
        {
            try
            {
                await _supabase.From<ExperienceItem>().Where(x => x.Id == id).Delete();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Supabase DeleteExperience error: {ex.Message}");
            }
        }
    }

    // ==========================================
    // CONTACT MESSAGES
    // ==========================================
    public async Task<List<ContactMessage>> GetMessagesAsync(bool forceReload = false)
    {
        if (_cachedMessages != null && !forceReload)
            return _cachedMessages;

        if (_supabase != null)
        {
            try
            {
                var response = await _supabase.From<ContactMessage>()
                    .Order(x => x.CreatedAt, Supabase.Postgrest.Constants.Ordering.Descending)
                    .Get();

                if (response?.Models != null)
                {
                    _cachedMessages = response.Models;
                    return _cachedMessages;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Supabase GetMessages error: {ex.Message}");
            }
        }

        return _cachedMessages ??= new();
    }

    public async Task<bool> SendMessageAsync(ContactMessage message)
    {
        message.CreatedAt = DateTime.UtcNow;
        _cachedMessages ??= new();
        _cachedMessages.Insert(0, message);

        if (_supabase != null)
        {
            try
            {
                await _supabase.From<ContactMessage>().Insert(message);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Supabase SendMessage error: {ex.Message}");
                // Return true anyway so the user gets pleasant UI in offline/demo mode
                return true;
            }
        }

        return true;
    }

    public async Task MarkMessageReadAsync(Guid id, bool isRead)
    {
        var msg = _cachedMessages?.FirstOrDefault(m => m.Id == id);
        if (msg != null)
            msg.IsRead = isRead;

        if (_supabase != null)
        {
            try
            {
                await _supabase.From<ContactMessage>()
                    .Where(x => x.Id == id)
                    .Set(x => x.IsRead, isRead)
                    .Update();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Supabase MarkMessageRead error: {ex.Message}");
            }
        }
    }

    public async Task DeleteMessageAsync(Guid id)
    {
        _cachedMessages?.RemoveAll(m => m.Id == id);

        if (_supabase != null)
        {
            try
            {
                await _supabase.From<ContactMessage>().Where(x => x.Id == id).Delete();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Supabase DeleteMessage error: {ex.Message}");
            }
        }
    }
}
