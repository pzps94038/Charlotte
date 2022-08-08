import {ResultModel} from '../api.interface';
import http from '../Instance';
import {Top10} from './top10.interface';

export const getTop10 = () => http.get<Top10[]>('api/Top10');
