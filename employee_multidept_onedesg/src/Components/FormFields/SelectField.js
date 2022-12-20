import { Fragment } from "react";
import { Controller } from "react-hook-form";
import Select from 'react-select';
import { errorType } from "../../constants";

function SelectField({ control, rules, label, name, onSelect, defaultValue, options, type, multi, optional, readOnly, placeholder, parentDivClass, childClass, int }) {

  const handleChange = (val, onChange) => {
    if (onSelect)
      onSelect(val);

    if (multi)
      return onChange(val.map(c => c?.value))
    else
      return onChange(val.value)
  };

  const handleValue = (value) => {
    if (multi)
      return options.filter(c => value?.includes?.(c.value))
    else
      return options.find(c => c.value === value)
  };

  return (
    <>
      <Controller
        control={control}
        rules={rules}
        defaultValue={defaultValue !== undefined ? defaultValue : ""}
        name={name}
        render={({ field: { onChange, value, ref }, fieldState: { error } }) => {
          return (
            <div className={`form-group ${parentDivClass ? parentDivClass : ''}`}>
              {(type !== "hidden") && (label ? <label>{label} {optional && <small>{(optional)}</small>}</label> : '')}
              <div className="textfield-block">
                <div className={childClass ? childClass : 'bootstrap-select'}>
                  <div className="fg-line">
                    <Select inputRef={ref}
                      isDisabled={readOnly}
                      value={handleValue(int !== undefined ? parseInt(value) ? parseInt(value) : value : value)}
                      onChange={val => handleChange(val, onChange)}
                      options={options}
                      placeholder={placeholder ? placeholder : label}
                      isMulti={multi} />
                  </div>
                  {errorType?.map(type => {
                    return <Fragment key={type}>{error?.type === type && error?.message !== "" ? <span className="error">{error?.message}</span> : null}</Fragment>
                  })}
                </div>
              </div>
            </div>
          );
        }}
      />
    </>
  )
}

export default SelectField;