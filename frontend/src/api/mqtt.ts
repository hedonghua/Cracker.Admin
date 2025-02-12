import request from "@/utils/request";
import { ApplicationResult } from "#/data";

export interface MqttTokenBody {
  token: string;
  expired: number;
}

// 获取mqtt token
export function getMqttToken(code: string) {
  return request.post<any, ApplicationResult<MqttTokenBody>>(
    "/api/mqtt/getMqttToken?code=" + code
  );
}
