using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Portfolio.Wasm.Models;

[Table("profile_settings")]
public class ProfileSettings : BaseModel
{
    [PrimaryKey("id", false)] public Guid Id { get; set; } = Guid.NewGuid();
    [Column("full_name")] public string FullName { get; set; } = "Erick John";
    [Column("headline")] public string Headline { get; set; } = ".NET & Full-Stack Software Developer";
    [Column("status_text")] public string StatusText { get; set; } = "Available for Opportunities";
    [Column("bio")] public string? Bio { get; set; } = "Building modern, high-performance web applications with Blazor and .NET.";
    [Column("avatar_url")] public string? AvatarUrl { get; set; }
    [Column("resume_url")] public string? ResumeUrl { get; set; }
    [Column("github_url")] public string? GithubUrl { get; set; }
    [Column("linkedin_url")] public string? LinkedinUrl { get; set; }
    [Column("email_address")] public string? EmailAddress { get; set; }
    [Column("updated_at")] public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

[Table("projects")]
public class ProjectItem : BaseModel
{
    [PrimaryKey("id", false)] public Guid Id { get; set; } = Guid.NewGuid();
    [Column("title")] public string Title { get; set; } = string.Empty;
    [Column("slug")] public string Slug { get; set; } = string.Empty;
    [Column("summary")] public string Summary { get; set; } = string.Empty;
    [Column("content_markdown")] public string? ContentMarkdown { get; set; }
    [Column("thumbnail_url")] public string? ThumbnailUrl { get; set; }
    [Column("gallery_urls")] public List<string> GalleryUrls { get; set; } = new();
    [Column("tech_tags")] public List<string> TechTags { get; set; } = new();
    [Column("live_url")] public string? LiveUrl { get; set; }
    [Column("repo_url")] public string? RepoUrl { get; set; }
    [Column("is_featured")] public bool IsFeatured { get; set; }
    [Column("display_order")] public int DisplayOrder { get; set; }
    [Column("created_at")] public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

[Table("skills")]
public class SkillItem : BaseModel
{
    [PrimaryKey("id", false)] public Guid Id { get; set; } = Guid.NewGuid();
    [Column("name")] public string Name { get; set; } = string.Empty;
    [Column("category")] public string Category { get; set; } = "Backend"; // 'Backend', 'Frontend', 'Tools'
    [Column("icon_name")] public string? IconName { get; set; }
    [Column("display_order")] public int DisplayOrder { get; set; }
}

[Table("experiences")]
public class ExperienceItem : BaseModel
{
    [PrimaryKey("id", false)] public Guid Id { get; set; } = Guid.NewGuid();
    [Column("role_or_degree")] public string RoleOrDegree { get; set; } = string.Empty;
    [Column("organization")] public string Organization { get; set; } = string.Empty;
    [Column("period")] public string Period { get; set; } = string.Empty;
    [Column("description")] public string? Description { get; set; }
    [Column("type")] public string Type { get; set; } = "Work"; // 'Work', 'Education', 'Certification'
    [Column("display_order")] public int DisplayOrder { get; set; }
}

[Table("messages")]
public class ContactMessage : BaseModel
{
    [PrimaryKey("id", false)] public Guid Id { get; set; } = Guid.NewGuid();
    [Column("sender_name")] public string SenderName { get; set; } = string.Empty;
    [Column("sender_email")] public string SenderEmail { get; set; } = string.Empty;
    [Column("message")] public string Message { get; set; } = string.Empty;
    [Column("is_read")] public bool IsRead { get; set; }
    [Column("created_at")] public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
