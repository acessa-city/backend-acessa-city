using AcessaCity.Business.App.Reports;
using AcessaCity.Business.App.UserNotifications;
using AcessaCity.Business.Interfaces;
using AcessaCity.Business.Interfaces.Repository;
using AcessaCity.Business.Interfaces.Service;
using AcessaCity.Business.Notifications;
using AcessaCity.Business.Services;
using AcessaCity.Data.Context;
using AcessaCity.Data.Repository;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.DependencyInjection;

namespace AcessaCity.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();

            var firebaseApp = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("./Configuration/GoogleCredentials.json")
            });
            var firebaseAuth = FirebaseAuth.DefaultInstance;

            services.AddSingleton(firebaseApp);
            services.AddSingleton(firebaseAuth);
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICityHallRepository, CityHallRepository>();
            services.AddScoped<ICityHallService, CityHallService>();
            services.AddScoped<IReportStatusRepository, ReportStatusRepository>();
            services.AddScoped<IReportClassificationRepository, ReportClassificationRepository>();
            services.AddScoped<IReportClassificationService, ReportClassificationService>();
            services.AddScoped<IUrgencyLevelRepository, UrgencyLevelRepository>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IReportCommentaryRepository, ReportCommentaryRepository>();
            services.AddScoped<IReportCommentaryService, ReportCommentaryService>();        
            services.AddScoped<IReportAttachmentRepository, ReportAttachmentRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IReportInteractionHistoryRepository, ReportInteractionHistoryRepository>();
            services.AddScoped<IReportInteractionHistoryService, ReportInteractionHistoryService>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IReportInteractionHistoryCommentaryRepository, ReportInteractionHistoryCommentaryRepository>();
            services.AddScoped<IReportInteractionHistoryCommentaryService, ReportInteractionHistoryCommentaryService>();
            services.AddScoped<IReportInProgressRepository, ReportInProgressRepository>();
            services.AddScoped<IReportInProgressService, ReportInProgressService>();
            services.AddScoped<IUserNotificationService, UserNotificationService>();
            services.AddScoped<IUserNotificationRepository, UserNotificationRepository>();
            

            //business logic class
            services.AddScoped<ReportStatusUpdate, ReportStatusUpdate>();
            services.AddScoped<ReportCoordinatorUpdate, ReportCoordinatorUpdate>();
            services.AddScoped<NotificationHandler, NotificationHandler>();

            //external services
            services.AddScoped<IGeolocationService, GeolocationService>();


            services.AddScoped<INotifier, Notifier>();

            return services;
        }
        
    }
}