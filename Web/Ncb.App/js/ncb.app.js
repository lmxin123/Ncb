//
/* ==============================================================
 *app公用
 * ============================================================= */
+(function($, doc, common) {
	// 全局配置
	common.config = {
		appName: '农村宝',
		apiUrl: 'http://1m801r3448.imwork.net:21517', //api请求的根地址
		rootPath: 'http://1m801r3448.imwork.net:34153' //服务器的静态资源地址
		//apiUrl: 'http://192.168.0.100:5001', 
		//rootPath: ''http://192.168.0.100:5000'
	};
	
	common.keys={
		USERINFO_KEY:'USERINFO_KEY'
	};

	$.ready(function() {
		var title = doc.querySelector('title');
		title.innerText = common.config.appName + '-' + title.innerText;
		$.qsa('[data-appname]').forEach(function(item) {
			item.innerText = common.config.appName;
		});

		common.error = function(msg) {
			plus.nativeUI.toast(msg || '出错了', {
				verticalAlign: 'top'
			});
		};
	});

	/**
	 *创建原先webview,
	 * @param {String} url 地址
	 * @param {String} titleText 标题
	 * @param {Object} extra 扩展参数
	 * @returns {Object} webview
	 */
	common.createWebview = function(url, titleText, autoBack, style, extra) {
		var defaultStyle = {
			top: '0px',
			bottom: '51px',
			titleNView: { //配置原生标题
				'backgroundColor': '#f7f7f7',
				'titleText': titleText,
				'titleColor': '#000000',
				autoBackButton: typeof(autoBack) === "undefined" ? true : autoBack,
				splitLine: {
					color: '#cccccc'
				}
			}
		};
		if(style) {
			$.extend(true, defaultStyle, style);
		}
		if(extra) {
			$.extend(true, extra, common.USERINFO);
		} else {
			extra = common.USERINFO;
		}
		return plus.webview.create(url, url, defaultStyle, extra);
	};

	common.openWebviewWithTile = function(url, titleText, callback) {
		mui.openWindowWithTitle({
			url: url,
			id: url,
			waiting: {
				autoShow: false
			},
			extras: common.USERINFO
		}, {

			height: "65px", //导航栏高度值
			backgroundColor: "#f7f7f7", //导航栏背景色
			bottomBorderColor: "#cccccc", //底部边线颜色
			title: { //标题配置
				text: titleText, //标题文字
				position: { //绘制文本的目标区域，参考：http://www.html5plus.org/doc/zh_cn/nativeobj.html#plus.nativeObj.Rect
					top: '8px',
					left: 0,
					width: "100%",
					height: "100%"
				}
			},
			back: { //左上角返回箭头
				image: { //图片格式
					base64Data: NCB_ICON.back, //加载图片的Base64编码格式数据 base64Data 和 imgSRC 必须指定一个.否则不显示返回箭头
					sprite: { //图片源的绘制区域，参考：http://www.html5plus.org/doc/zh_cn/nativeobj.html#plus.nativeObj.Rect
						top: '0px',
						left: '0px',
						width: '100%',
						height: '100%'
					},
					position: { //绘制图片的目标区域，参考：http://www.html5plus.org/doc/zh_cn/nativeobj.html#plus.nativeObj.Rect
						top: "25px",
						left: "10px",
						width: "24px",
						height: "24px"
					}
				},
				click: callback
			}
		})
	};

	common.saveRemotePic = function(picurl, name) {
		var dtask = plus.downloader.createDownload(picurl, {
			filename: "_downloads/" + name
		}, function(d, status) {
			// 下载完成
			if(status == 200) {
				plus.gallery.save(d.filename, function() {
					mui.toast('保存成功!');
				}, function() {
					mui.toast('保存失败，请重试！');
				});
			} else {
				mui.toast("图片下载失败: " + status);
			}

		});
		dtask.start();
	};

	common.saveLocalPic = function(picurl) {
		plus.nativeUI.actionSheet({
			cancel: '取消',
			buttons: [{
				title: '保存到相册'
			}]
		}, function(e) {
			var index = e.index;
			if(e.index === 1) {
				plus.gallery.save(picurl, function() {
					mui.toast('保存成功');
				}, function() {
					mui.toast('保存失败，请重试！');
				});
			}
		});
	};

	/**
	 *获取网卡物理地址
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
		}
		return mac;
	};
	common.update = function() {
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
							var url = rsp.Data.DowloadUrl + '?mac=' + common.getMac();
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
	};

})(mui, document, common);

//
/* ==============================================================
 *用户模块
 * ============================================================= */
(function(com, mui) {
	var initUserInfo = function(callback) {
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

		mui.post(common.config.apiUrl + '/userInfo/getUserInfo', device, function(rsp) {
			postFlag = true;
			if(rsp.Success) {
				common.USERINFO = {
					id: rsp.Data.ID,
					name: rsp.Data.Name,
					phone: rsp.Data.PhoneNumber,
					expiryDate: rsp.Data.ExpiryDate,
					isPaid: rsp.Data.IsPaid,
					isExpiring: rsp.Data.IsExpiring,
					isExpired: rsp.Data.IsExpired
				};
				common.setLocalObject(common.keys.USERINFO_KEY, common.USERINFO);
				callback && callback();
			}
			else{
				mui.toast('初始化数据失败！');
			}
		});
	};

	common.getUserInfo = function(isNew, callback) {
		if(isNew) {
			initUserInfo(callback);
		} else {
			common.USERINFO = common.getLocalObject(common.keys.USERINFO_KEY);
			if(common.USERINFO) {
				callback && callback();
			} else {
				initUserInfo(callback);
			}
		}
	};
})(common, mui)