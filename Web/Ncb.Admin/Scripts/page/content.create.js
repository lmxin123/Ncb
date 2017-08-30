+(function ($, doc) {
    'use strict';

    $(function () {
        var errClass = 'input-validation-error',
            btnCreate = $('#btnCreate'),
            accessType = $('#AccessType'),
            freeDate = $('#FreeDate'),
            form = $('#createContentForm'),
            fdContainer = $('#freeDataContainer'),
            fileContainer = $('#fileContainer');

        freeDate.datetimepicker({
            minView: "month",
            format: "yyyy-mm-dd",
            language: 'zh-CN',
            autoclose: true
        });

        var ueditor = UE.getEditor('Content');
        var contentContainer;

        ueditor.ready(function () {
            var content = $('#Content');
            contentContainer = content.find('div.edui-editor-iframeholder');
            contentContainer.find('iframe').click(function () {
                common.clearValErrorMsg(this.id);
            });
        });

        accessType.change(function () {
            if (this.selectedIndex === 2)
                fdContainer.removeClass('hidden');
            else
                fdContainer.addClass('hidden');
        });

        btnCreate.ajaxClick({
            formSelector: '#createContentForm',
            loadingText: "正在保存...",
            onBefore: function ($ajaxBtn) {
                $ajaxBtn.$form.valid();

                var validError = false, errorMsg = [];
                if (accessType.val() === 'TimeFree' && freeDate.val() === '') {
                    freeDate.removeClass('valid');
                    common.setValErrorMsg(freeDate.attr('id'), '请输入免费到期日期');
                    validError = true;
                }
                else {
                    common.clearValErrorMsg(freeDate.attr('id'));
                }

                if (ueditor.getContent() === '') {
                    common.setValErrorMsg(contentContainer.attr('id'), '请输入内容');
                    validError = true;
                }
                else {
                    common.clearValErrorMsg(contentContainer.attr('id'));
                }

                //if (fileContainer.find('tr').length === 0) {
                //    fileContainer.addClass(errClass);
                //    errorMsg.push('请选择封面图片！');
                //    validError = true;
                //}
                //else if (fileContainer.find(':disabled').length > 0 && fileContainer.find('.error').text() !== '') {
                //    fileContainer.find('.error').each(function () {
                //        var $this = $(this);
                //        if ($this.text().trim().length) {
                //            $this.parents('tr').addClass(errClass)
                //        }
                //    });
                //    errorMsg.push('请取消无效文件！');
                //    validError = true;
                //}
                //else {
                //    fileContainer.find('.error').each(function () {
                //        var $this = $(this);
                //        if ($this.text().trim().length) {
                //            $this.parents('tr').removeClass(errClass)
                //        }
                //    });
                //}

                if (validError) {
                    if (errorMsg.length > 0)
                        common.alert(errorMsg.join('<br>'));
                    return false;
                }

                return {
                    Content: ueditor.getContent()
                };
            },
            onSuccess: function (result, $ajaxBtn) {
                if (result.Success) {
                    common.alert('保存成功！');
                    var p = common.getUrlParameToJSON();
                    if (common.isEmptyObject(p)) {
                        $ajaxBtn.reset();
                    }
                    else {
                        $ajaxBtn.setLoadingText('保存成功,正在刷新界面...');
                        setTimeout(function () {
                            location.reload();
                        }, 3000);
                    }
                }
                else {
                    common.alert(result.Message);
                    $ajaxBtn.reset();
                }
            }
        });


    });
})($, document);
