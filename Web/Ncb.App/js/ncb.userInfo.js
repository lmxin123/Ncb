(function(com, mui) {

	common.initUserInfo = function(callback) {
		var userInfo = localStorage.getItem('USERINFO');
		if(userInfo != null) {
			common.USERINFO = JSON.parse(userInfo);
			callback && callback();
		} else {
			common.getUserInfo(callback);
		}
	}

	var postFlag = false;
	common.getUserInfo = function(callback) {
		if(postFlag) return;

		var id = common.getMac();
		mui.getJSON(common.config.apiUrl + '/userInfo/getUserInfo?id=' + id, function(rsp) {
			postFlag = true;
			if(rsp.Success) {
				common.USERINFO = {
					id: rsp.Data.ID,
					name: rsp.Data.Name,
					phone: rsp.Data.PhoneNumber,
					amount: rsp.Data.Amount,
					expiryDate: rsp.Data.ExpiryDate,
					cid: rsp.Data.CategoryID
				};
				localStorage.setItem('USERINFO', JSON.stringify(common.USERINFO));
				callback && callback();
			} else {
				common.setDevice(callback);
			}
		});
	}
	/**
	 *创建原先setDevice,
	 * @param {Object} 参数
	 * @returns {Object} void
	 */
	common.setDevice = function(callback) {
		var device = {
			AppId: plus.runtime.appid,
			Imei: plus.device.imei, //设备标识
			Platform: mui.os.android ? 'Android' : 'IPhone',
			Model: plus.device.model, //设备型号
			AppVersion: plus.runtime.version,
			PlusVersion: plus.runtime.innerVersion, //基座版本号
			OsVersion: mui.os.version,
			NetType: plus.networkinfo.getCurrentType(),
			Id: common.getMac(),
			Vendor: plus.device.vendor
		};

		mui.post(common.config.apiUrl + '/device/create', device, function(rsp) {
			if(rsp.Success) {
				common.USERINFO.id = device.Id;
				common.USERINFO.cid = 1;
				localStorage.setItem('USERINFO', JSON.stringify(common.USERINFO));
				callback && callback();
			};
		});
	};

	/**
	 *获取网卡物理地址getMac,
	 * @returns {String} mac
	 */
	common.getMac = function() {
		var mac = "xxx-xxx-xxx-xxx";

		if(plus.os.name == "Android") {
			var Context = plus.android.importClass("android.content.Context");
			var WifiManager = plus.android.importClass("android.net.wifi.WifiManager");
			var wifiManager = plus.android.runtimeMainActivity().getSystemService(Context.WIFI_SERVICE);
			var WifiInfo = plus.android.importClass("android.net.wifi.WifiInfo");
			var wifiInfo = wifiManager.getConnectionInfo();
			mac = wifiInfo.getMacAddress();

			return mac;
		}
	}

	mui.plusReady(function() {
		common.initUserInfo();
	});
})(common, mui)