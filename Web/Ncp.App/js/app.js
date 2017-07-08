(function(mui,com,doc){
	  // 全局配置
    com.config = {
    	name:'农村宝',
        apiPath: 'http://192.168.32.1/app/',//当前站点的api请求根路径
        absPath: location.host, //当前站点的绝对路径
        homeUrl: '/index' //当前站点的首页地址
    };
    
	mui.ready(function(){
		mui('title').text();
		
	});
})(mui,common,document);