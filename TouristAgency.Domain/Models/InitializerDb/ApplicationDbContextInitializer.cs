using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Domain.App_Start;
using TouristAgency.Domain.Models.EfModels;
using TouristAgency.WebUI;
using TouristAgency.WebUI.Models;

namespace TouristAgency.Domain.Models.InitializerDb
{
    public class ApplicationDbContextInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var hotelsTypes = new List<HotelsType>
            {
                new HotelsType {
                    HotelType = "Апарт-отель"
                },
                new HotelsType {
                    HotelType = "Мотель "
                },
                new HotelsType {
                    HotelType = "Гостевой дом"
                },
                new HotelsType {
                    HotelType = "Отели-курорты"
                },
                new HotelsType {
                    HotelType = "Бизнес-отели"
                }
            };
            hotelsTypes.ForEach(hT => context.HotelsTypes.Add(hT));

            var orderStatuses = new List<OrderStatus>
            {
                new OrderStatus{
                    OrdersStatus = "Зарегистрирован"
                },
                new OrderStatus{
                    OrdersStatus = "Оплачен"
                },
                new OrderStatus{
                    OrdersStatus = "Отменен"
                }
            };
            orderStatuses.ForEach(oS => context.OrderStatuses.Add(oS));

            var typesTour = new List<TypesTour>
            {
                new TypesTour{
                    TypeTour="Отдых"
                },
                new TypesTour{
                    TypeTour="Экскурсия"
                },
                new TypesTour{
                    TypeTour="Шоппинг"
                }
            };
            typesTour.ForEach(tT => context.TypesTours.Add(tT));

            var userManager = new ApplicationUserManager(new UserStore<ApplicationMyUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // создаем две роли
            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "user" };
            var role3 = new IdentityRole { Name = "manager" };

            // добавляем роли в бд
            roleManager.Create(role1);
            roleManager.Create(role2);
            roleManager.Create(role3);

            // создаем пользователей
            var admin = new ApplicationMyUser { Email = "somemail@mail.ru", UserName = "somemail@mail.ru" };
            string password = "ad46D_ewr3";
            var result = userManager.Create(admin, password);

            // если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role2.Name);
                userManager.AddToRole(admin.Id, role3.Name);
            }
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
