import mqtt from "mqtt";
import { ElNotification } from "element-plus";
import { ref } from "vue";
import { getMqttToken } from "@/api/mqtt";
import dayjs from "dayjs";
import Utils from "@/utils/tool";

const clientId = `mqtt_${Math.random().toString(16).slice(3)}`;

//添加订阅的主题
const topics = ["notification"];

export const useMqtt = () => {
  const connectUrl = import.meta.env.VITE_MQTT_SERVER;
  const client = ref<mqtt.MqttClient>();
  const token = ref<string>();

  const connect = () => {
    const mqttTokenString = localStorage.getItem("mqtt_token");
    if (mqttTokenString) {
      const mqttToken = JSON.parse(mqttTokenString);
      const isValid =
        mqttToken?.token &&
        dayjs.unix(mqttToken?.expired ?? 0).isAfter(dayjs());
      if (isValid) {
        token.value = mqttToken.token;
        doConnect();
        return;
      }
    }

    const fingerprint = Utils.getBrowserCode();
    getMqttToken(fingerprint!).then((res) => {
      localStorage.setItem("mqtt_token", JSON.stringify(res.data));
      token.value = res.data.token;
      doConnect();
    });

    function doConnect() {
      client.value = mqtt.connect(connectUrl, {
        clientId,
        clean: true,
        connectTimeout: 4000,
        username: token.value,
        reconnectPeriod: 1000,
      });

      client.value.on("connect", () => {
        console.log("MQTT Connected");
        client.value?.subscribe(topics, () => {
          console.log(`Subscribe to topics '${topics.join(",")}'`);
        });
      });
      client.value.on("message", (topic, payload) => {
        switch (topic) {
          case "notification":
            const json = JSON.parse(payload.toString());
            ElNotification({
              title: getNoticificationTitle(json["type"]),
              message: json["message"],
              type: json["type"],
            });
            break;
        }
      });
    }
  };

  const getNoticificationTitle = (type: string) => {
    switch (type) {
      case "success":
        return "成功";
      case "warning":
        return "警告";
      case "info":
        return "信息";
      case "error":
        return "错误";
    }
  };

  return { client, connect };
};
