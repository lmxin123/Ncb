+function(n,t){"use strict";n.module("deviceApp",["ui.bootstrap"]).controller("DeviceCtrl",function(n,i){n.pageSize=20;n.totalItems=0;n.currentPage=1;n.device={};n.List=[];n.getList=function(){i({url:"/device/getList",method:"POST",headers:{"Content-Type":undefined},transformRequest:function(i){return i=new FormData(t.getElementById("queryForm")),i.append("pageIndex",n.currentPage),i.append("pageSize",n.pageSize),i}}).success(function(t){t.Success?(t.Data.length===0&&common.alert("未查到数据！"),n.List=t.Data,n.totalItems=t.TotalCount):common.alert(t.Message)}).error(function(){common.alert("出现错误或者网络异常！");n.List=[];n.totalItems=0})};n.deviceModal=function(){if(arguments.length!==0){for(var t=0;t<n.List.length;t++)if(n.List[t].ID===arguments[0]){n.device.ID=arguments[0];n.device.Name=n.List[t].Name;n.device.Address=n.List[t].Address;n.device.DeviceCode=n.List[t].DeviceCode;n.device.Province=n.List[t].Province;n.device.City=n.List[t].City;n.device.Region=n.List[t].Region;n.device.Contact=n.List[t].Contact;n.device.Phone=n.List[t].Phone;n.device.Email=n.List[t].Email;n.device.Count=n.List[t].Count;n.device.IsOnlineDesplay=n.List[t].IsOnlineDesplay;n.device.CreateDateDisplay=n.List[t].CreateDateDisplay;n.device.Operator=n.List[t].Operator;n.device.Remark=n.List[t].Remark;r.deviceModal.modal("show");break}}else i.get("/device/getDeviceCode").success(function(t){t.Success?(n.device.DeviceCode=t.Data,r.deviceModal.modal("show")):common.alert(t.Message)}).error(function(t){common.alert(t.Message||"出错了");n.List=[];n.totalItems=0})};n.delete=function(t,r){var u=$(r.target);if(!u.data("delete")){u.text("确定要删除吗");u.data("delete",!0).css({color:"red"});return}common.alert("正在删除...",function(){i.post("/device/delete",{id:t}).success(function(t){t.Success?(common.alert("删除成功！"),n.getList()):(common.alert(t.Message),u.text("删除"),u.data("delete",!1).css({color:""}))}).error(function(){common.alert("出现错误或者网络异常！");n.List=[];n.totalItems=0})})};$("#btnSaveDevice").ajaxClick({formSelector:"#deviceForm",onBefore:function(t){t.$form.attr("action","/device/"+(n.device.ID?"update":"create"))},onCompleted:function(t,i){t.Success?(n.getList(),common.alert("保存成功"),r.deviceModal.modal("hide")):common.alert("保存失败："+t.Message);i.reset()}});var r={deviceModal:$("#deviceModal"),deviceForm:$("#deviceForm")};r.deviceModal.on("hide.bs.modal",function(){n.device={}})}).filter("areaFilter",function(){return exAreaCode})}(window.angular,document);