import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Calendar } from "../models/Calendar";
import { Observable } from "rxjs";
import { Product } from "../models/Product";
import { PaginatedList } from "../models/PaginatedList";
import { Calories } from "../models/Calories";
import { CaloriesByDay } from "../models/CaloriesByDay";
import { SpecialData } from "../models/SpecialData";
import { FindRequestsDto } from "../models/FindRequestsDto";
import { CreateRequestDto } from "../models/CreateRequestDto";
import { RequestDto } from "../models/Request";
import { ChangeRequestDto } from "../models/ChangeRequestDto";

@Injectable({
    providedIn: 'root',
  })
export class RequestService {
    refRequest: string = "https://localhost:7171/api/Request";

    constructor(private httpClient: HttpClient, private router: Router) {
    }

    public getRequests(dto: FindRequestsDto) {
        return this.httpClient.get<PaginatedList<RequestDto>>(this.refRequest + "/?creatorId=" + dto.creatorId 
            + "&requestType="+dto.requestType);
    }

    public createRequest(reqDto: CreateRequestDto) {
        return this.httpClient.post(this.refRequest, reqDto);
    }

    public getRequest(id: number) {
        return this.httpClient.get<CreateRequestDto>(this.refRequest + "/" + id);
    }

    public changeStatus(dto: ChangeRequestDto) {
        return this.httpClient.put(this.refRequest + "/status", dto);
    }
  
}