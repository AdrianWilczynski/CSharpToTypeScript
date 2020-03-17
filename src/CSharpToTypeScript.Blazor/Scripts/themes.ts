import * as oceanicNext from 'monaco-themes/themes/Oceanic Next.json';
import * as blackboard from 'monaco-themes/themes/Blackboard.json';
import * as cloudsMidnight from 'monaco-themes/themes/Clouds Midnight.json';
import * as merbivore from 'monaco-themes/themes/Merbivore.json';
import * as monokai from 'monaco-themes/themes/Monokai.json'
import * as nightOwl from 'monaco-themes/themes/Night Owl.json'
import * as tomorrowNight from 'monaco-themes/themes/Tomorrow-Night.json'

export const themes = [
    { name: 'oceanic-next', data: oceanicNext },
    { name: 'blackboard', data: blackboard },
    { name: 'clouds-midnight', data: cloudsMidnight },
    { name: 'merbivore', data: merbivore },
    { name: 'monokai', data: monokai },
    { name: 'night-owl', data: nightOwl },
    { name: 'tomorrow-night', data: tomorrowNight },
    { name: 'one-dark', data: getOneDark() },
    { name: 'nord', data: getNord() }
]

export const visualStudioDarkBackgroundColor = '#1e1e1e';

function getOneDark() {
    // Generated from "One Dark theme for Sublime Text" by Andres Michel (https://github.com/andresmichel/one-dark-theme) (MIT)
    // using https://bitwiser.in/monaco-themes/ by Brijesh Bittu (https://github.com/brijeshb42/monaco-themes/) (MIT)
    return {
        'base': 'vs-dark',
        'inherit': true,
        'rules': [
            {
                'foreground': 'abb2bf',
                'token': 'text'
            },
            {
                'foreground': 'abb2bf',
                'token': 'source'
            },
            {
                'foreground': 'adb7c9',
                'token': 'variable.parameter.function'
            },
            {
                'foreground': '5f697a',
                'fontStyle': ' italic',
                'token': 'comment'
            },
            {
                'foreground': '5f697a',
                'fontStyle': ' italic',
                'token': 'punctuation.definition.comment'
            },
            {
                'foreground': 'adb7c9',
                'token': 'none'
            },
            {
                'foreground': 'adb7c9',
                'token': 'keyword.operator'
            },
            {
                'foreground': 'cd74e8',
                'token': 'keyword'
            },
            {
                'foreground': 'eb6772',
                'token': 'variable'
            },
            {
                'foreground': '5cb3fa',
                'token': 'entity.name.function'
            },
            {
                'foreground': '5cb3fa',
                'token': 'meta.require'
            },
            {
                'foreground': '5cb3fa',
                'token': 'support.function.any-method'
            },
            {
                'foreground': 'f0c678',
                'token': 'support.class'
            },
            {
                'foreground': 'f0c678',
                'token': 'entity.name.class'
            },
            {
                'foreground': 'f0c678',
                'token': 'entity.name.type.class'
            },
            {
                'foreground': 'adb7c9',
                'token': 'meta.class'
            },
            {
                'foreground': '5cb3fa',
                'token': 'keyword.other.special-method'
            },
            {
                'foreground': 'cd74e8',
                'token': 'storage'
            },
            {
                'foreground': '5ebfcc',
                'token': 'support.function'
            },
            {
                'foreground': '9acc76',
                'token': 'string'
            },
            {
                'foreground': '9acc76',
                'token': 'constant.other.symbol'
            },
            {
                'foreground': '9acc76',
                'token': 'entity.other.inherited-class'
            },
            {
                'foreground': 'db9d63',
                'token': 'constant.numeric'
            },
            {
                'foreground': 'db9d63',
                'token': 'none'
            },
            {
                'foreground': 'db9d63',
                'token': 'none'
            },
            {
                'foreground': 'db9d63',
                'token': 'constant'
            },
            {
                'foreground': 'eb6772',
                'token': 'entity.name.tag'
            },
            {
                'foreground': 'db9d63',
                'token': 'entity.other.attribute-name'
            },
            {
                'foreground': 'db9d63',
                'token': 'entity.other.attribute-name.id'
            },
            {
                'foreground': 'db9d63',
                'token': 'punctuation.definition.entity'
            },
            {
                'foreground': 'cd74e8',
                'token': 'meta.selector'
            },
            {
                'foreground': 'db9d63',
                'token': 'none'
            },
            {
                'foreground': '5cb3fa',
                'token': 'markup.heading punctuation.definition.heading'
            },
            {
                'foreground': '5cb3fa',
                'token': 'entity.name.section'
            },
            {
                'foreground': 'db9d63',
                'token': 'keyword.other.unit'
            },
            {
                'foreground': 'f0c678',
                'token': 'markup.bold'
            },
            {
                'foreground': 'f0c678',
                'token': 'punctuation.definition.bold'
            },
            {
                'foreground': 'cd74e8',
                'token': 'markup.italic'
            },
            {
                'foreground': 'cd74e8',
                'token': 'punctuation.definition.italic'
            },
            {
                'foreground': '9acc76',
                'token': 'markup.raw.inline'
            },
            {
                'foreground': 'eb6772',
                'token': 'string.other.link'
            },
            {
                'foreground': 'eb6772',
                'token': 'punctuation.definition.string.end.markdown'
            },
            {
                'foreground': 'db9d63',
                'token': 'meta.link'
            },
            {
                'foreground': 'eb6772',
                'token': 'markup.list'
            },
            {
                'foreground': 'db9d63',
                'token': 'markup.quote'
            },
            {
                'foreground': 'adb7c9',
                'background': '515151',
                'token': 'meta.separator'
            },
            {
                'foreground': '9acc76',
                'token': 'markup.inserted'
            },
            {
                'foreground': 'eb6772',
                'token': 'markup.deleted'
            },
            {
                'foreground': 'cd74e8',
                'token': 'markup.changed'
            },
            {
                'foreground': '5ebfcc',
                'token': 'constant.other.color'
            },
            {
                'foreground': '5ebfcc',
                'token': 'string.regexp'
            },
            {
                'foreground': '5ebfcc',
                'token': 'constant.character.escape'
            },
            {
                'foreground': 'c94e42',
                'token': 'punctuation.section.embedded'
            },
            {
                'foreground': 'c94e42',
                'token': 'variable.interpolation'
            },
            {
                'foreground': 'ffffff',
                'background': 'e05252',
                'token': 'invalid.illegal'
            },
            {
                'foreground': '2d2d2d',
                'background': 'f99157',
                'token': 'invalid.broken'
            },
            {
                'foreground': '2c323d',
                'background': 'd27b53',
                'token': 'invalid.deprecated'
            },
            {
                'foreground': '2c323d',
                'background': '747369',
                'token': 'invalid.unimplemented'
            },
            {
                'foreground': 'eb6772',
                'token': 'source.json                           meta.structure.dictionary.json                              string.quoted.double.json'
            },
            {
                'foreground': '9acc76',
                'token': 'source.json                       meta.structure.dictionary.json                           meta.structure.dictionary.value.json                       string.quoted.double.json'
            },
            {
                'foreground': 'eb6772',
                'token': 'source.json                           meta.structure.dictionary.json                          meta.structure.dictionary.value.json                        meta.structure.dictionary.json                      string.quoted.double.json'
            },
            {
                'foreground': '9acc76',
                'token': 'source.json                       meta.structure.dictionary.json                        meta.structure.dictionary.value.json                       meta.structure.dictionary.json                          meta.structure.dictionary.value.json                            string.quoted.double.json'
            },
            {
                'foreground': 'cd74e8',
                'token': 'text.html.laravel-blade                        source.php.embedded.line.html                     entity.name.tag.laravel-blade'
            },
            {
                'foreground': 'cd74e8',
                'token': 'text.html.laravel-blade                         source.php.embedded.line.html                    support.constant.laravel-blade'
            },
            {
                'foreground': 'db9d63',
                'token': 'source.python meta.function.python meta.function.parameters.python variable.parameter.function.python'
            },
            {
                'foreground': '5ebfcc',
                'token': 'source.python meta.function-call.python support.type.python'
            },
            {
                'foreground': 'cd74e8',
                'token': 'source.python keyword.operator.logical.python'
            },
            {
                'foreground': 'f0c678',
                'token': 'source.python meta.class.python punctuation.definition.inheritance.begin.python'
            },
            {
                'foreground': 'f0c678',
                'token': 'source.python meta.class.python punctuation.definition.inheritance.end.python'
            },
            {
                'foreground': 'db9d63',
                'token': 'source.python meta.function-call.python meta.function-call.arguments.python variable.parameter.function.python'
            },
            {
                'foreground': 'db9d63',
                'token': 'text.html.basic                   source.php.embedded.block.html                             support.constant.std.php'
            },
            {
                'foreground': 'f0c678',
                'token': 'text.html.basic                              source.php.embedded.block.html                               meta.namespace.php                              entity.name.type.namespace.php'
            },
            {
                'foreground': 'db9d63',
                'token': 'source.js                              meta.function.js                       support.constant.js'
            },
            {
                'foreground': 'cd74e8',
                'token': 'text.html.basic`                               source.php.embedded.block.html                        constant.other.php'
            },
            {
                'foreground': 'db9d63',
                'token': 'text.html.basic                              source.php.embedded.block.html                               support.other.namespace.php'
            },
            {
                'foreground': 'adb7c9',
                'token': 'text.tex.latex                               meta.function.environment.math.latex                               string.other.math.block.environment.latex                               meta.definition.label.latex                               variable.parameter.definition.label.latex'
            },
            {
                'foreground': 'cd74e8',
                'fontStyle': ' italic',
                'token': 'text.tex.latex                           meta.function.emph.latex                              markup.italic.emph.latex'
            },
            {
                'foreground': 'adb7c9',
                'token': 'source.js                          variable.other.readwrite.js'
            },
            {
                'foreground': 'adb7c9',
                'token': 'source.js                         meta.function-call.with-arguments.js                        variable.function.js'
            },
            {
                'foreground': 'adb7c9',
                'token': 'source.js                            meta.group.braces.round                           meta.group.braces.curly                             meta.function-call.method.without-arguments.js                    variable.function.js'
            },
            {
                'foreground': 'adb7c9',
                'token': 'source.js                            meta.group.braces.round                            meta.group.braces.curly                            variable.other.object.js'
            },
            {
                'foreground': 'adb7c9',
                'token': 'source.js                             meta.group.braces.round                           meta.group.braces.curly                            constant.other.object.key.js                            string.unquoted.label.js'
            },
            {
                'foreground': 'adb7c9',
                'token': 'source.js                       meta.group.braces.round                            meta.group.braces.curly                           constant.other.object.key.js                         punctuation.separator.key-value.js'
            },
            {
                'foreground': 'adb7c9',
                'token': 'source.js                            meta.group.braces.round                           meta.group.braces.curly                           meta.function-call.method.with-arguments.js                 variable.function.js'
            },
            {
                'foreground': 'adb7c9',
                'token': 'source.js                            meta.function-call.method.with-arguments.js                        variable.function.js'
            },
            {
                'foreground': 'adb7c9',
                'token': 'source.js                       meta.function-call.method.without-arguments.js                       variable.function.js'
            }
        ],
        'colors': {
            'editor.foreground': '#abb2bf', // modified
            'editor.background': '#2B303B',
            'editor.selectionBackground': '#bbccf51b',
            'editor.inactiveSelectionBackground': '#bbccf51b',
            'editor.lineHighlightBackground': '#8cc2fc0b',
            'editorCursor.foreground': '#528bff',
            'editorWhitespace.foreground': '#747369',
            'editorIndentGuide.background': '#464c55',
            'editorIndentGuide.activeBackground': '#464c55',
            'editor.selectionHighlightBorder': '#bbccf51b'
        }
    };
}

function getNord() {
    // Generated from "Nord Textmate" by Ryan Bloom (https://github.com/ryanbloom/nord-textmate) (Unlicense)
    // using https://bitwiser.in/monaco-themes/ by Brijesh Bittu (https://github.com/brijeshb42/monaco-themes/) (MIT)
    return {
        'base': 'vs-dark',
        'inherit': true,
        'rules': [
            {
                'foreground': '616e88',
                'token': 'comment'
            },
            {
                'foreground': 'a3be8c',
                'token': 'string'
            },
            {
                'foreground': 'b48ead',
                'token': 'constant.numeric'
            },
            {
                'foreground': '81a1c1',
                'token': 'constant.language'
            },
            {
                'foreground': '81a1c1',
                'token': 'keyword'
            },
            {
                'foreground': '81a1c1',
                'token': 'storage'
            },
            {
                'foreground': '81a1c1',
                'token': 'storage.type'
            },
            {
                'foreground': '8fbcbb',
                'token': 'entity.name.class'
            },
            {
                'foreground': '8fbcbb',
                'fontStyle': '  bold',
                'token': 'entity.other.inherited-class'
            },
            {
                'foreground': '88c0d0',
                'token': 'entity.name.function'
            },
            {
                'foreground': '81a1c1',
                'token': 'entity.name.tag'
            },
            {
                'foreground': '8fbcbb',
                'token': 'entity.other.attribute-name'
            },
            {
                'foreground': '88c0d0',
                'token': 'support.function'
            },
            {
                'foreground': 'f8f8f0',
                'background': 'f92672',
                'token': 'invalid'
            },
            {
                'foreground': 'f8f8f0',
                'background': 'ae81ff',
                'token': 'invalid.deprecated'
            },
            {
                'foreground': 'b48ead',
                'token': 'constant.color.other.rgb-value'
            },
            {
                'foreground': 'ebcb8b',
                'token': 'constant.character.escape'
            },
            {
                'foreground': '8fbcbb',
                'token': 'variable.other.constant'
            }
        ],
        'colors': {
            'editor.foreground': '#D8DEE9',
            'editor.background': '#2E3440',
            'editor.selectionBackground': '#434C5ECC',
            'editor.lineHighlightBackground': '#3B4252',
            'editorCursor.foreground': '#D8DEE9',
            'editorWhitespace.foreground': '#434C5ECC'
        }
    };
}