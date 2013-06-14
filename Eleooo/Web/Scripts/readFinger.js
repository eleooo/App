function readFinger(renderTo, msgTarget) {
    try {
        var spDeviceType = 0;
        var spComPort = 0;
        var spBaudRate = 0;
        var message = "指纹读入成功!";
        aspnetForm.finger.GetImgCode(spDeviceType, spComPort, spBaudRate);
        if (aspnetForm.finger.ErrorCode != 0)
            message = "读取失败:" + aspnetForm.finger.Msg;
        $('#' + renderTo).val(aspnetForm.finger.FingerCode);
        if (msgTarget)
            $('#' + msgTarget).html(message);
    }
    catch (ex) {
        if (msgTarget)
            $('#' + msgTarget).html("指纹读入失败!<br />" + ex.Message);
        alert(ex.Message);
    }
}