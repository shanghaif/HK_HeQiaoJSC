export function handleSignClick(){
	console.log(1)
}
//1志愿者，2督导员下午 3.志愿者下午，4.督导员（0）
// 本地存储打卡信息
export function setSignInfo(info,id){
	if(id==1)//志愿者
	{
		var volun = {main:[info]}
		uni.setStorageSync("volun",JSON.stringify(volun));
	}
	else if(id==2)
	{
		var volun = {main:[info]}
		uni.setStorageSync("signInfoPM",JSON.stringify(volun));
	}
	else if(id==3)
	{
		var volun = {main:[info]}
		uni.setStorageSync("volunPM",JSON.stringify(volun));
	}
	else{
		var signInfo = {main:[info]}
		uni.setStorageSync("signInfo",JSON.stringify(signInfo));
	}
	
}
// 本地添加打卡信息
export function addSignInfo(info,sign,id){
	if(id==1)
	{
		sign.main.push(info);
		uni.setStorageSync("volun",JSON.stringify(sign));
	}
	else if(id==2)
	{
		sign.main.push(info);
		uni.setStorageSync("signInfoPM",JSON.stringify(sign));
	}
	else if(id==3)
	{
		sign.main.push(info);
		uni.setStorageSync("volunPM",JSON.stringify(sign));
	}
	else{
		sign.main.push(info);
		uni.setStorageSync("signInfo",JSON.stringify(sign));
	}
	
}
// 本地获取打卡信息
export function getSignInfo(id){
	if(id==1)//志愿者
	{
		let sign = uni.getStorageSync("volun");
		if(!sign){
			return;
		}
		return JSON.parse(sign)
	}
	else if(id==2){
		let sign = uni.getStorageSync("signInfoPM");
		if(!sign){
			return;
		}
		return JSON.parse(sign)
	}
	else if(id==3){
		let sign = uni.getStorageSync("volunPM");
		if(!sign){
			return;
		}
		return JSON.parse(sign)
	}
	else{
		let sign = uni.getStorageSync("signInfo");
		if(!sign){
			return;
		}
		return JSON.parse(sign)
	}
	
}
// 本地打卡信息清理
export function delSignInfo(id){
	if(id==1)
	{
		uni.removeStorage({
			key: 'volun',
			// success(){
			// 	uni.showToast({title:"重置成功"})
			// }
		});
	}else{
		uni.removeStorage({
			key: 'signInfo',
			// success(){
			// 	uni.showToast({title:"重置成功"})
			// }
		});
	}
	
}
// 打卡信息
export function getInfo(signInfo){
	var nowT = new Date();
	var info = {mode:signInfo.mode, nowT:nowT,address:signInfo.address,time:signInfo.time,latitude:signInfo.latitude,longitude:signInfo.longitude,remarks:signInfo.remarks};
	return info;
}

// 腾讯位置服务key 值
export const key = "VEEBZ-HJL34-U3LUY-XUBOX-NSUF7-E4BRF";


