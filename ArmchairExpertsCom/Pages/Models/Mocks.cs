using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Npgsql;

namespace ArmchairExpertsCom.Pages
{
    public static class Mocks
    {
        private static Dictionary<Type, IModel[]> Objects = new Dictionary<Type, IModel[]>
        {
            {
                typeof(Book), new Book[]
                {
                    new Book
                    {
                        Id = 1,
                        Title = "Героой нашего времени",
                        Description =
                            "Первый в русской прозе лирико-психологический роман, написанный Михаилом Юрьевичем Лермонтовым в 1838-1840 годах. Классика русской литературы. Впервые роман был издан в Санкт-Петербурге в типографии Ильи Глазунова и Кº в 1840 г. в 2 книгах.",
                        Authors = "М. Ю. Лермонтов",
                        Image = new Image
                        {
                            Id = 1,
                            Path = "/img/geroy_nashego_vremeni.png"
                        },
                        Rating = 10
                    },
                    new Book
                    {
                        Id = 2,
                        Title = "Евгений Онегин",
                        Description =
                            "Роман в стихах русского поэта Александра Сергеевича Пушкина, написанный в 1823-1830 годах, одно из самых значительных произведений русской словесности. Повествование ведётся от имени безымянного автора, который представился добрым приятелем Онегина. По словам Белинского, Пушкин назвал «Евгения Онегина» романом в стихах, поскольку в нём изображена «жизнь во всей её прозаической действительности»",
                        Authors = "А. С. Пушкин",
                        Image = new Image
                        {
                            Id = 2,
                            Path = "/img/eugene_onegin.jpg"
                        },
                        Rating = 9
                    },
                    new Book
                    {
                        Id = 3,
                        Title = "Отцы и дети",
                        Description =
                            "Роман И. С. Тургенева, написанный в 1860-1861 годах и опубликованный в 1862 году. В обстановке «великих реформ» книга стала сенсацией и привлекла к себе всеобщее внимание, а образ главного героя Евгения Базарова был воспринят как воплощение нового, пореформенного поколения, став примером для подражания молодёжи 1860-х гг. Свойственные Базарову бескомпромиссность, отсутствие преклонения перед авторитетами и старыми истинами, приоритет полезного над прекрасным стали идеалами первого поколения пореформенной интеллигенции.",
                        Authors = "И. В. Тургенев",
                        Image = new Image
                        {
                            Id = 3,
                            Path = "/img/otsy_i_deti.jpg"
                        },
                        Rating = 8
                    }
                }
            }
        };
        
        public static IEnumerable<T> All<T>()
            where T : IModel
        {
            foreach (var obj in Objects[typeof(Book)])
                yield return (T) obj;
        }
        
        public static T Get<T>(int id)
            where T : IModel
        {
            return (T)Objects[typeof(T)].FirstOrDefault(book => book.Id == id);
        }
    }
}