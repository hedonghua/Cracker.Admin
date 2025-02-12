import mqtt from "mqtt";
import { ElNotification } from "element-plus";

const clientId = `mqtt_${Math.random().toString(16).slice(3)}`;

const connectUrl = import.meta.env.VITE_MQTT_SERVER;
const client = mqtt.connect(connectUrl, {
  clientId,
  clean: true,
  connectTimeout: 4000,
  username: "admin",
  password: "123qwe*",
  reconnectPeriod: 1000,
});

//添加订阅的主题
const topics = ["notification"];
client.on("connect", () => {
  console.log("MQTT Connected");
  client.subscribe(topics, () => {
    console.log(`Subscribe to topics '${topics.join(',')}'`);
  });
});
client.on("message", (topic, payload) => {
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


export default client;