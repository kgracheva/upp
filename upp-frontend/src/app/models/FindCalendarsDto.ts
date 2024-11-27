export interface FindCalendarsDto {
    date: Date | null;
    userId: number;
    mealTypeId: number;
    page: number;
    size: number;
}