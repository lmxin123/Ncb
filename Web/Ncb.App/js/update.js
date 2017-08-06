(function() {
	function update() {
		var server = common.config.apiUrl + "/update/check"; //获取升级描述文件服务器地址
		mui.getJSON(server, {
			mac: common.getMac(),
			appid: plus.runtime.appid,
			version: plus.runtime.version,
			imei: plus.device.imei
		}, function(rsp) {
			if(rsp.Success) {
				if(rsp.Data.Updated) {
					plus.nativeUI.confirm(rsp.Data.Note, function(event) {
						if(0 == event.index) {
							var url = rsp.Data.url + '&mac=' + common.getMac();
							plus.runtime.openURL(url);
						}
					}, rsp.Data.Title, ["立即更新", "取　　消"]);
				} else {
					mui.toast('己是最新版本！');
				}
			} else {
				mui.toast(rsp.Message);
			}
		});
	}

	mui.plusReady(function() {
		var update = document.getElementById('update');
		if(update) {
			update.addEventListener('tap', update);
		} else {
			update();
		}

	});

});