using Microsoft.EntityFrameworkCore;
using BankApi.Models;

public class HeaderMenuInitializer
{
    private readonly BankDbContext context;
    public HeaderMenuInitializer(BankDbContext context)
    {
        this.context = context;
    }

    public async Task SeedMenu()
    {
        await context.Database.MigrateAsync();
        if (await context.LevelOneMenu.AnyAsync())
        {
            return;
        }
        var menuTabs = new List<LevelOneMenu>
        {
            // Первый таб
            //***************************************************************************************************************************
            new LevelOneMenu
        {
            Name = "individuals",
            Text = "Частным клиентам",
            SortOrder = 1, // Первый главный таб
            Items = new List<LevelTwoMenu>
            {
        // 1. Кредиты
        new LevelTwoMenu
        {
            Name = "credit",
            Text = "Кредиты",
            Icon = "percent",
            SortOrder = 1, // Первая категория по порядку
            Items = new List<LevelThreeMenu>
            {
                new LevelThreeMenu { Name = "cash_credit", Text = "Кредит наличными", SortOrder = 1 },
                new LevelThreeMenu { Name = "car_credit", Text = "Автокредит", SortOrder = 2 },
                new LevelThreeMenu { Name = "refinancing_credit", Text = "Рефинансирование кредитов", SortOrder = 3 },
                new LevelThreeMenu { Name = "education_credit", Text = "Образовательный кредит с господдержкой", SortOrder = 4 }
            }
        },
        // 2. Ипотека
        new LevelTwoMenu
        {
            Name = "house",
            Text = "Ипотека",
            Icon = "house",
            SortOrder = 2,
            Items = new List<LevelThreeMenu>
            {
                new LevelThreeMenu { Name = "mortgage_selection", Text = "Подбор ипотеки", SortOrder = 1 },
                new LevelThreeMenu { Name = "secondary_housing", Text = "Вторичное жильё", SortOrder = 2 },
                new LevelThreeMenu { Name = "house_mortgage", Text = "Ипотека на дом", SortOrder = 3 },
                new LevelThreeMenu { Name = "family_mortgage", Text = "Семейная", SortOrder = 4 },
                new LevelThreeMenu { Name = "new_buildings", Text = "Новостройки", SortOrder = 5 },
                new LevelThreeMenu { Name = "it_mortgage", Text = "Для ИТ-специалистов", SortOrder = 6 },
                new LevelThreeMenu { Name = "commercial_real_estate", Text = "Коммерческая недвижимость", SortOrder = 7 }
            }
        },
        // 3. Карты
        new LevelTwoMenu
        {
            Name = "card",
            Text = "Карты",
            Icon = "card",
            SortOrder = 3,
            Items = new List<LevelThreeMenu>
            {
                new LevelThreeMenu { Name = "debit_cards", Text = "Дебетовые", SortOrder = 1 },
                new LevelThreeMenu { Name = "credit_cards", Text = "Кредитные", SortOrder = 2 },
                new LevelThreeMenu { Name = "mir_pay", Text = "Mir Pay", SortOrder = 3 }
            }
        },
        // 4. Вклады
        new LevelTwoMenu
        {
            Name = "deposits",
            Text = "Вклады",
            Icon = "bag",
            SortOrder = 4,
            Items = new List<LevelThreeMenu>
            {
                new LevelThreeMenu { Name = "new_money_deposit", Text = "Вклад для новых денег", SortOrder = 1 },
                new LevelThreeMenu { Name = "pension_deposit", Text = "Пенсионный вклад", SortOrder = 2 },
                new LevelThreeMenu { Name = "social_deposit", Text = "Социальный вклад", SortOrder = 3 }
            }
        },
        // 5. Инвестиции
        new LevelTwoMenu
        {
            Name = "investments",
            Text = "Инвестиции",
            Icon = "bar",
            SortOrder = 5,
            Items = new List<LevelThreeMenu>
            {
                new LevelThreeMenu { Name = "brokerage_account", Text = "Брокерский счёт", SortOrder = 1 },
                new LevelThreeMenu { Name = "iis", Text = "ИИС", SortOrder = 2 },
                new LevelThreeMenu { Name = "securities_catalog", Text = "Каталог бумаг", SortOrder = 3 },
                new LevelThreeMenu { Name = "currency_exchange", Text = "Обмен валюты на бирже", SortOrder = 4 },
                new LevelThreeMenu { Name = "precious_securities", Text = "Драгоценные бумаги", SortOrder = 5 },
                new LevelThreeMenu { Name = "tariffs", Text = "Тарифы", SortOrder = 6 }
            }
        },
        // 6. Страхование
        new LevelTwoMenu
        {
            Name = "insurance",
            Text = "Страхование",
            Icon = "shield",
            SortOrder = 6,
            Items = new List<LevelThreeMenu>
            {
                new LevelThreeMenu { Name = "osago", Text = "ОСАГО", SortOrder = 1 },
                new LevelThreeMenu { Name = "kasko", Text = "Каско", SortOrder = 2 },
                new LevelThreeMenu { Name = "health_insurance", Text = "Здоровье", SortOrder = 3 },
                new LevelThreeMenu { Name = "roadside_assistance", Text = "Помощь на дорогах", SortOrder = 4 },
                new LevelThreeMenu { Name = "home_insurance", Text = "Для дома и квартиры", SortOrder = 5 }
            }
        },
        // 7. Путешествия
        new LevelTwoMenu
        {
            Name = "journey",
            Text = "Путешествия",
            Icon = "plane",
            SortOrder = 7,
            Items = new List<LevelThreeMenu>
            {
                new LevelThreeMenu { Name = "flight_tickets", Text = "Авиабилеты", SortOrder = 1 },
                new LevelThreeMenu { Name = "tours", Text = "Туры", SortOrder = 2 },
                new LevelThreeMenu { Name = "trips", Text = "Поездки", SortOrder = 3 },
                new LevelThreeMenu { Name = "hotels", Text = "Отели", SortOrder = 4 }
            }
        },
        // 8. Сервисы
        new LevelTwoMenu
        {
            Name = "services",
            Text = "Сервисы",
            Icon = "services",
            SortOrder = 8,
            Items = new List<LevelThreeMenu>
            {
                new LevelThreeMenu { Name = "mobile_app", Text = "Мобильное приложение", SortOrder = 1 },
                new LevelThreeMenu { Name = "card_readiness_check", Text = "Проверка готовности карты", SortOrder = 2 },
                new LevelThreeMenu { Name = "certificates_statements", Text = "Справки и выписки", SortOrder = 3 }
            }
        }
    }
        },
        // Второй таб
        //***************************************************************************************************************************
        new LevelOneMenu
{
    Name = "business",
    Text = "Для бизнеса",
    SortOrder = 2, // Второй главный таб на фронтенде
    Items = new List<LevelTwoMenu>
    {
        // 1. Расчётный счёт
        new LevelTwoMenu
        {
            Name = "current_account",
            Text = "Расчётный счёт",
            Icon = "money_box",
            SortOrder = 1,
            Items = new List<LevelThreeMenu>
            {
                new LevelThreeMenu { Name = "checking_account_ip", Text = "Расчётный счёт ИП", SortOrder = 1 },
                new LevelThreeMenu { Name = "checking_account_ooo", Text = "Расчётный счёт ООО", SortOrder = 2 },
                new LevelThreeMenu { Name = "tariffs", Text = "Тарифы", SortOrder = 3 }
            }
        },
        // 2. Регистрация бизнеса
        new LevelTwoMenu
        {
            Name = "registration",
            Text = "Регистрация бизнеса",
            Icon = "startup",
            SortOrder = 2,
            Items = new List<LevelThreeMenu>
            {
                new LevelThreeMenu { Name = "registration_ip", Text = "Регистрация ИП", SortOrder = 1 },
                new LevelThreeMenu { Name = "registration_ooo", Text = "Регистрация ООО", SortOrder = 2 },
                new LevelThreeMenu { Name = "start_solutions", Text = "Решения для старта", SortOrder = 3 },
                new LevelThreeMenu { Name = "self_employed", Text = "Самозанятые", SortOrder = 4 }
            }
        },
        // 3. Бухгалтерия
        new LevelTwoMenu
        {
            Name = "accounting",
            Text = "Бухгалтерия",
            Icon = "calculator",
            SortOrder = 3,
            Items = new List<LevelThreeMenu>
            {
                new LevelThreeMenu { Name = "ens_balance", Text = "Баланс ЕНС", SortOrder = 1 },
                new LevelThreeMenu { Name = "tax_piggy_bank", Text = "Налоговая копилка", SortOrder = 2 },
                new LevelThreeMenu { Name = "accounting_usn_income", Text = "Бухгалтерия УСН Доходы", SortOrder = 3 },
                new LevelThreeMenu { Name = "ausn_service", Text = "Сервис для АУСН", SortOrder = 4 }
            }
        },
        // 4. Бизнес-карты
        new LevelTwoMenu
        {
            Name = "business_cards",
            Text = "Карты",
            Icon = "business_card",
            SortOrder = 4,
            Items = new List<LevelThreeMenu>
            {
                new LevelThreeMenu { Name = "mir_in_pocket", Text = "Мир в кармане", SortOrder = 1 },
                new LevelThreeMenu { Name = "mir_card", Text = "Мир", SortOrder = 2 },
                new LevelThreeMenu { Name = "mir_supreme", Text = "Mir Supreme", SortOrder = 3 }
            }
        },
        // 5. Приём платежей
        new LevelTwoMenu
        {
            Name = "payments",
            Text = "Приём платежей",
            Icon = "qr_code",
            SortOrder = 5,
            Items = new List<LevelThreeMenu>
            {
                new LevelThreeMenu { Name = "merchant_acquiring", Text = "Торговый эквайринг", SortOrder = 1 },
                new LevelThreeMenu { Name = "internet_acquiring", Text = "Интернет эквайринг", SortOrder = 2 },
                new LevelThreeMenu { Name = "sbp_payments", Text = "Приём платежей по СБП", SortOrder = 3 },
                new LevelThreeMenu { Name = "qr_payments", Text = "Приём платежей по QR", SortOrder = 4 },
                new LevelThreeMenu { Name = "smartphone_payments", Text = "Приём платежей через смартфон", SortOrder = 5 },
                new LevelThreeMenu { Name = "online_cash_registers", Text = "Онлайн кассы", SortOrder = 6 }
            }
        },
        // 6. Отраслевые решения
        new LevelTwoMenu
        {
            Name = "solutions",
            Text = "Отраслевые решения",
            Icon = "truck",
            SortOrder = 6,
            Items = new List<LevelThreeMenu>
            {
                new LevelThreeMenu { Name = "marketplaces", Text = "Маркетплейсы", SortOrder = 1 },
                new LevelThreeMenu { Name = "retail", Text = "Розница", SortOrder = 2 },
                new LevelThreeMenu { Name = "car_dealers", Text = "Автодилеры", SortOrder = 3 },
                new LevelThreeMenu { Name = "travel_agency", Text = "Агентство путешествий", SortOrder = 4 }
            }
        }
    }
},
// Третье меню
//****************************************************************************************************************************
new LevelOneMenu
{
    Name = "private",
    Text = "Private Banking",
    SortOrder = 3, // Третий главный таб на фронтенде
    Items = new List<LevelTwoMenu>
    {
        // 1. Банковские услуги
        new LevelTwoMenu
        {
            Name = "banking",
            Text = "Банковские услуги",
            Icon = "premiumBankIcon",
            SortOrder = 1,
            Items = new List<LevelThreeMenu>
            {
                new LevelThreeMenu { Name = "premium_cashback_cards", Text = "Премиальные карты с кэшбэком", SortOrder = 1 },
                new LevelThreeMenu { Name = "high_rate_deposits", Text = "Вклады с повышенной ставкой", SortOrder = 2 },
                new LevelThreeMenu { Name = "borderless_currency_transfers", Text = "Валютные переводы без границ", SortOrder = 3 },
                new LevelThreeMenu { Name = "safe_deposit_boxes", Text = "Сейфовые ячейки", SortOrder = 4 },
                new LevelThreeMenu { Name = "individual_lending_terms", Text = "Индивидуальные условия кредитования", SortOrder = 5 }
            }
        },
        // 2. Инвестиции (Wealth)
        new LevelTwoMenu
        {
            Name = "wealth",
            Text = "Инвестиции",
            Icon = "diamondIcon",
            SortOrder = 2,
            Items = new List<LevelThreeMenu>
            {
                new LevelThreeMenu { Name = "trust_management", Text = "Доверительное управление", SortOrder = 1 },
                new LevelThreeMenu { Name = "structured_products", Text = "Структурные продукты", SortOrder = 2 },
                new LevelThreeMenu { Name = "international_markets_access", Text = "Доступ к международным рынкам", SortOrder = 3 },
                new LevelThreeMenu { Name = "alternative_investments", Text = "Альтернативные инвестиции (арт, вино)", SortOrder = 4 },
                new LevelThreeMenu { Name = "unit_linked_insurance", Text = "Инвестиционное страхование жизни", SortOrder = 5 }
            }
        },
        // 3. Консалтинг
        new LevelTwoMenu
        {
            Name = "consulting",
            Text = "Консалтинг",
            Icon = "strategyIcon",
            SortOrder = 3,
            Items = new List<LevelThreeMenu>
            {
                new LevelThreeMenu { Name = "tax_consulting", Text = "Налоговое консультирование", SortOrder = 1 },
                new LevelThreeMenu { Name = "family_office_legal", Text = "Юридическая поддержка (Family Office)", SortOrder = 2 },
                new LevelThreeMenu { Name = "asset_inheritance", Text = "Наследование активов", SortOrder = 3 },
                new LevelThreeMenu { Name = "currency_control", Text = "Валютный контроль", SortOrder = 4 },
                new LevelThreeMenu { Name = "cfc_reporting", Text = "Подготовка отчетности для КИК", SortOrder = 5 }
            }
        },
        // 4. Лайфстайл
        new LevelTwoMenu
        {
            Name = "lifestyle",
            Text = "Лайфстайл",
            Icon = "conciergeIcon",
            SortOrder = 4,
            Items = new List<LevelThreeMenu>
            {
                new LevelThreeMenu { Name = "concierge_24_7", Text = "Персональный консьерж 24/7", SortOrder = 1 },
                new LevelThreeMenu { Name = "airport_lounge_access", Text = "Доступ в бизнес-залы аэропортов", SortOrder = 2 },
                new LevelThreeMenu { Name = "hotel_restaurant_privileges", Text = "Привилегии в отелях и ресторанах", SortOrder = 3 },
                new LevelThreeMenu { Name = "jet_yacht_charter", Text = "Аренда бизнес-джетов и яхт", SortOrder = 4 },
                new LevelThreeMenu { Name = "private_events_invitations", Text = "Приглашения на закрытые мероприятия", SortOrder = 5 }
            }
        }
    }
}
};
        await context.LevelOneMenu.AddRangeAsync(menuTabs);
        await context.SaveChangesAsync();
    }
}