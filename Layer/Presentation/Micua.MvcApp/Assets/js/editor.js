/// <reference path="../lib/jquery/jquery-1.11.0.js" />
var MEditor = {
    $editor: null,
    tools: {
        '|': '</div><div class="btn-group">',
        'undo': '<button type="button" class="btn btn-default" data-action="execute" data-command="undo"><i class="fa fa-undo"></i></button>',
        'redo': '<button type="button" class="btn btn-default" data-action="execute" data-command="redo"><i class="fa fa-repeat"></i></button>',
        'bold': '<button type="button" class="btn btn-default" data-action="execute" data-command="bold"><i class="fa fa-bold"></i></button>',
        'italic': '<button type="button" class="btn btn-default" data-action="execute" data-command="italic"><i class="fa fa-italic"></i></button>',
        'underline': '<button type="button" class="btn btn-default" data-action="execute" data-command="underline"><i class="fa fa-underline"></i></button>',
        'strikethrough': '<button type="button" class="btn btn-default" data-action="execute" data-command="strikethrough"><i class="fa fa-strikethrough"></i></button>',
        'justifyleft': '<button type="button" class="btn btn-default" data-action="execute" data-command="justifyleft"><i class="fa fa-align-left"></i></button>',
        'justifycenter': '<button type="button" class="btn btn-default" data-action="execute" data-command="justifycenter"><i class="fa fa-align-center"></i></button>',
        'justifyright': '<button type="button" class="btn btn-default" data-action="execute" data-command="justifyright"><i class="fa fa-align-right"></i></button>',
        'justifyjustify': '<button type="button" class="btn btn-default" data-action="execute" data-command="justifyjustify"><i class="fa fa-align-justify"></i></button>',
        'indent': '<button type="button" class="btn btn-default" data-action="execute" data-command="indent"><i class="fa fa-indent"></i></button>',
        'outdent': '<button type="button" class="btn btn-default" data-action="execute" data-command="outdent"><i class="fa fa-outdent"></i></button>',
        'removeformat': '<button type="button" class="btn btn-default" data-action="execute" data-command="removeformat"><i class="fa fa-eraser"></i></button>',
        'link': '<button type="button" class="btn btn-default" data-action="execute" data-command="link"><i class="fa fa-link"></i></button>',
        'unlink': '<button type="button" class="btn btn-default" data-action="execute" data-command="unlink"><i class="fa fa-unlink"></i></button>',
        'head': '<div class="btn-group"><button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown"><i class="fa fa-header"></i><span class="caret"></span></button><ul class="dropdown-menu"><li><a href="#" data-action="wrap" data-command="h1"><b>H1</b></a></li><li><a href="#" data-action="wrap" data-command="h2"><b>H2</b></a></li><li><a href="#" data-action="wrap" data-command="h3"><b>H3</b></a></li><li><a href="#" data-action="wrap" data-command="h4"><b>H4</b></a></li><li><a href="#" data-action="wrap" data-command="h5"><b>H5</b></a></li><li><a href="#" data-action="wrap" data-command="h6"><b>H6</b></a></li></ul></div>',
        'paragraph': '<button type="button" class="btn btn-default" data-action="wrap" data-command="p"><b>P</b></button>',
        'superscript': '<button type="button" class="btn btn-default" data-action="execute" data-command="superscript"><i class="fa fa-superscript"></i></button>',
        'subscript': '<button type="button" class="btn btn-default" data-action="execute" data-command="subscript"><i class="fa fa-subscript"></i></button>',
        'insertorderedlist': '<button type="button" class="btn btn-default" data-action="execute" data-command="insertorderedlist"><i class="fa fa-list-ol"></i></button>',
        'insertunorderedlist': '<button type="button" class="btn btn-default" data-action="execute" data-command="insertunorderedlist"><i class="fa fa-list-ul"></i></button>',
    },
    initEditor: function (selector, toolBars) {
        var $textArea = $(selector).hide();
        var editorHtml = '<div class="editor-container"><div class="editor-toolbar btn-toolbar" role="toolbar"><div class="btn-group">{$toolBar}</div></div><div class="editor-body" contenteditable="true"><p>&nbsp;</p></div></div>';
        var toolBarHtml = '';
        if (!toolBars) {
            toolBars = ['source', '|', 'undo', 'redo', '|', 'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', '|', 'bold', 'italic', 'underline', 'strikethrough', '|', 'insertorderedlist', 'insertunorderedlist', '|', 'indent', 'outdent', '|', 'link', 'unlink', '|', 'head', 'paragraph', 'superscript', 'subscript', '|', 'removeformat'];
        }
        for (var i = 0; i < toolBars.length; i++) {
            var item = toolBars[i];
            var tool = this.tools[item];
            if (tool)
                toolBarHtml += this.tools[item];
        }
        $editor = $(editorHtml.replace('{$toolBar}', toolBarHtml)).insertAfter($textArea); // .attr('contenteditable', true)
        this.addListener('click', function () {
            //alert($(this).data('action'));
            //switch ($(this).data('action')) {
            //    case 'h1':
            //    case 'h2':
            //    case 'p':
            //        document.execCommand('formatBlock', false, '<' + $(this).data('role') + '>');
            //        break;
            //    default:
            //        document.execCommand($(this).data('role'), false, null);
            //        break;
            //}
            if ('execute' == $(this).data('action')) {
                document.execCommand($(this).data('command'), false, null);
            } else {
                document.execCommand('formatBlock', false, '<' + $(this).data('command') + '>');
            }
            return false;
        });
    },
    addListener: function (eventName, called) {
        $editor.find('[data-action]').bind(eventName, called);
    }
};