namespace Gardenation.Domain
{
    public enum WaterUrgency
    {
        Invalid = 0,
        DoNotWater = 1,
        WaterOptional = 2,
        WaterToday = 3,
        WaterOverdue = 4,
        WaterCritial = 5
    }
}