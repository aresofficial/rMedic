using rMedic.ViewModels.Attributes;

namespace rMedic.Models
{
    public enum Unit {
        [LocalizedDescription("Millimeters")]
        Millimeters, //милиметр
        [LocalizedDescription("Centimeters")]
        Centimeters, //сантиметр
        [LocalizedDescription("Meters")]
        Meters, //метр
        [LocalizedDescription("Suppositories")]
        Suppositories, //свечи
        [LocalizedDescription("Pills")]
        Pills, //таблетки
        [LocalizedDescription("Tubes")]
        Tubes, //тюбик
        [LocalizedDescription("Packs")]
        Packs, //упаковка
        [LocalizedDescription("Vials")]
        Vials, //флакон
        [LocalizedDescription("Pieces")]
        Pieces, //штуки
        [LocalizedDescription("Liters")]
        Liters, //литры
        [LocalizedDescription("Ampoules")]
        Ampoules, //ампулы
        [LocalizedDescription("Bottles")]
        Bottles //бутылки
    }
}
