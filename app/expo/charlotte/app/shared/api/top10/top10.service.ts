import { ResultModel } from "../api.model";
import http from "../Instance";
import { Top10Response } from "./top10.model";

export const getTop10 = () => http.get<Top10Response[]>("api/Top10");
