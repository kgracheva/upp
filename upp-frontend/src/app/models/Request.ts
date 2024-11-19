export interface RequestDto {
    id: number;
    entityId: number;
    requestType: number;
    comment: string;
    statusTypeId: number;
    statusTypeName: string;
    operatorId: number;
    operatorName: string;
    created: Date;
}