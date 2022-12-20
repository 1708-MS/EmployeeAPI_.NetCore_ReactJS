import { Fragment } from "react";
import { useController } from "react-hook-form";
import { errorType } from "../../constants";

function InputField({ control, normalize, defaultValue, rules, label, name, type, optional, readOnly, placeholder, fieldClass, parentDivClass, onInputChange }) {
  // use hooks
  const { field, fieldState: { error } } = useController({ name, control, rules, defaultValue });

  // handle onChange
  const handleOnChange = (e) => {
    if (normalize !== undefined) {
      field?.onChange(normalize(e.target.value))
      if (onInputChange !== undefined) {
        onInputChange(normalize(e.target.value));
      }
    } else {
      field?.onChange(type === 'number' ? parseInt(e.target.value) : e.target.value?.replace(/^\s*\s*$/, ''))
      if (onInputChange !== undefined) {
        onInputChange(type === 'number' ? parseInt(e.target.value) : e.target.value?.replace(/^\s*\s*$/, ''));
      }
    }
  };

  return (
    <>
      <div className={`form-group ${parentDivClass ? parentDivClass : ''}`}>
        {(type !== "hidden") && (label ? <label>{label} {optional && <small>{(optional)}</small>}</label> : '')}
        <div className="textfield-block">
          <input {...field}
            className={` ${fieldClass ? fieldClass : ' textfield  '} ${error !== undefined ? ' error ' : ''} form-control`}
            placeholder={placeholder ? placeholder : label}
            readOnly={readOnly !== undefined ? true : false}
            type={type}
            onChange={(e) => handleOnChange(e, field.onChange)}
          />
          {errorType?.map(type => {
            return <Fragment key={type}>{error?.type === type && error?.message !== "" ? <span className="error">{error?.message}</span> : null}</Fragment>
          })}
        </div>
      </div>
    </>
  )
};

InputField.defaultProps = {
  defaultValue: "",
};

export default InputField;
