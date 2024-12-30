export function useCodeGenerator() {
    const csharpTypeOptions = [
        { label: 'string', value: 'string' },
        { label: 'int', value: 'int' },
        { label: 'long', value: 'long' },
        { label: 'decimal', value: 'decimal' },
        { label: 'datetime', value: 'datetime' },
        { label: 'bool', value: 'bool' },
    ]

    const jsTypeOptions = [
        { label: 'string', value: 'string' },
        { label: 'number', value: 'number' },
        { label: 'boolean', value: 'boolean' },
        { label: 'Date', value: 'Date' }
    ]

    const htmlTypeOptions = [
        { label: '文本框', value: 'text' },
        { label: '文本域', value: 'textarea' },
        { label: '日期框', value: 'date' },
        { label: '下拉框', value: 'select' },
        { label: '复选框', value: 'checkbox' },
        { label: '单选框', value: 'radio' },
        { label: '富文本框', value: 'editor' },
        { label: '文件上传', value: 'file' },
    ]

    const trueOrFalseOptions = [
        { label: '是', value: true },
        { label: '否', value: false }
    ]

    const searchTypeOptions = [
        { label: '包含', value: 1 },
        { label: '等于', value: 2 },
    ]

    return {
        csharpTypeOptions,
        jsTypeOptions,
        htmlTypeOptions,
        trueOrFalseOptions,
        searchTypeOptions
    }
}