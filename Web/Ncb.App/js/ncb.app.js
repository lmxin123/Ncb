//
/* ==============================================================
 *app公用
 * ============================================================= */
+(function($, doc, common) {
	// 全局配置
	common.config = {
		appName: '农村宝',
		apiUrl: 'http://192.168.0.102', //api请求的根地址
		rootPath: 'http://192.168.0.102:81', //服务器的静态资源地址
		absPath: location.host, //当前站点的绝对路径
		homeUrl: '', //当前站点的首页地址
		cdnUrl: '' //资源分发地址，如css，js，img等静态文件单独放在一个服务器上
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
					base64Data: ICON.back, //加载图片的Base64编码格式数据 base64Data 和 imgSRC 必须指定一个.否则不显示返回箭头
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
	}

	
})(mui, document, common);