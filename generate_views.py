import os

def get_base_props():
    return [
        {"name": "ProductName", "label": "Tên Sản phẩm *"},
        {"name": "CategoryId", "label": "Category ID *", "type": "number"},
        {"name": "CompanyId", "label": "Company ID *", "type": "number"},
        {"name": "ImageUrl", "label": "URL Hình ảnh *"},
        {"name": "UnitPrice", "label": "Giá bán *", "type": "number", "step": "0.01"},
        {"name": "UnitsInStock", "label": "Tồn kho *", "type": "number"},
        {"name": "Description", "label": "Mô tả", "type": "textarea"}
    ]

def get_extra_props(category):
    if category == "Laptop":
        return [
            {"name": "LaptopType", "label": "Loại Laptop *"},
            {"name": "Os", "label": "Hệ điều hành *"},
            {"name": "ScreenResolution", "label": "Độ phân giải màn hình *"},
            {"name": "Length", "label": "Kích thước (inch) *", "type": "number", "step": "0.1"},
            {"name": "Weight", "label": "Trọng lượng (kg) *", "type": "number", "step": "0.1"},
            {"name": "CpuId", "label": "CPU ID *", "type": "number"},
            {"name": "GpuId", "label": "GPU ID *", "type": "number"},
            {"name": "RamId", "label": "RAM ID *", "type": "number"},
            {"name": "StorageId", "label": "Storage ID *", "type": "number"}
        ]
    elif category == "Cpu":
        return [
            {"name": "Cores", "label": "Số nhân *", "type": "number"},
            {"name": "Logicals", "label": "Số luồng *", "type": "number"},
            {"name": "Tdp", "label": "TDP (W) *", "type": "number", "step": "0.1"},
            {"name": "Socket", "label": "Socket *"},
            {"name": "Speed", "label": "Tốc độ xung nhịp (MHz) *", "type": "number"},
            {"name": "Turbo", "label": "Tốc độ Turbo (MHz) *", "type": "number"}
        ]
    elif category == "Gpu":
        return [
            {"name": "MemorySize", "label": "Dung lượng bộ nhớ (GB) *", "type": "number", "step": "0.1"},
            {"name": "MemoryType", "label": "Loại bộ nhớ *"},
            {"name": "Clock", "label": "Xung nhịp (MHz) *", "type": "number"},
            {"name": "UnifiedShader", "label": "Unified Shader *", "type": "number"},
            {"name": "Tmu", "label": "TMU *", "type": "number"},
            {"name": "Rop", "label": "ROP *", "type": "number"},
            {"name": "Bus", "label": "Bus *"},
            {"name": "Igpu", "label": "iGPU", "type": "checkbox"}
        ]
    elif category == "Ram":
        return [
            {"name": "Capacity", "label": "Dung lượng (GB) *", "type": "number"},
            {"name": "Gen", "label": "Thế hệ RAM *"},
            {"name": "Speed", "label": "Tốc độ (MHz) *", "type": "number"},
            {"name": "Kit", "label": "Kit *"}
        ]
    elif category == "Storage":
        return [
            {"name": "Capacity", "label": "Dung lượng (GB) *", "type": "number"},
            {"name": "MemoryType", "label": "Loại bộ nhớ *"},
            {"name": "InterfaceType", "label": "Loại giao tiếp *"},
            {"name": "ReadSpeed", "label": "Tốc độ đọc (MB/s) *", "type": "number"},
            {"name": "WriteSpeed", "label": "Tốc độ ghi (MB/s) *", "type": "number"}
        ]

def generate_form_html(props):
    html = ""
    for p in props:
        t = p.get("type", "text")
        step = p.get("step", "")
        if step:
            step_attr = f' step="{step}"'
        else:
            step_attr = ""

        if t == "textarea":
            html += f'''
            <div class="mb-3">
                <label asp-for="{p['name']}" class="form-label fw-medium">{p['label']}</label>
                <textarea asp-for="{p['name']}" class="form-control" rows="3"></textarea>
                <span asp-validation-for="{p['name']}" class="text-danger small"></span>
            </div>
            '''
        elif t == "checkbox":
            html += f'''
            <div class="mb-3 form-check">
                <input asp-for="{p['name']}" class="form-check-input" type="checkbox" />
                <label asp-for="{p['name']}" class="form-check-label fw-medium">{p['label']}</label>
                <span asp-validation-for="{p['name']}" class="text-danger small"></span>
            </div>
            '''
        else:
            html += f'''
            <div class="mb-3">
                <label asp-for="{p['name']}" class="form-label fw-medium">{p['label']}</label>
                <input asp-for="{p['name']}" type="{t}" class="form-control"{step_attr} />
                <span asp-validation-for="{p['name']}" class="text-danger small"></span>
            </div>
            '''
    return html

def create_view(category, is_edit):
    action = "Edit" if is_edit else "Create"
    model_namespace = "Update" if is_edit else "Create"
    dto_prefix = "Update" if is_edit else "Create"
    
    title = f"Sửa {category}" if is_edit else f"Thêm {category}"
    btn_text = "Lưu thay đổi" if is_edit else "Lưu Sản phẩm"

    base_props_html = generate_form_html(get_base_props())
    extra_props_html = generate_form_html(get_extra_props(category))

    content = f'''@model MVC18.DTOs.Products.{model_namespace}.{dto_prefix}{category}DTO
@{{
    ViewData["Title"] = "{title}";
    Layout = "~/Views/Shared/_Layout.cshtml";
}}

<div class="container-fluid py-4">
    <div class="container" style="max-width: 900px;">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2 class="mb-0">{title}</h2>
            <a asp-action="Products" asp-controller="Manager" class="btn btn-outline-secondary">
                <i class="fas fa-arrow-left me-2"></i>Quay lại
            </a>
        </div>

        <div class="card shadow-sm">
            <div class="card-body p-4">
                <form asp-action="{action}" asp-controller="{category}" method="post">
                    @Html.AntiForgeryToken()
                    
                    <div class="row">
                        <div class="col-md-6 border-end pe-4">
                            <h5 class="text-primary mb-4 pb-2 border-bottom">Thông tin Chung</h5>
                            {base_props_html}
                        </div>
                        <div class="col-md-6 ps-4">
                            <h5 class="text-success mb-4 pb-2 border-bottom">Thông số Kỹ thuật</h5>
                            {extra_props_html}
                        </div>
                    </div>

                    <div asp-validation-summary="ModelOnly" class="text-danger mb-3"></div>

                    <div class="d-flex justify-content-end mt-4 pt-3 border-top">
                        <button type="submit" class="btn btn-primary px-4">
                            <i class="fas fa-save me-2"></i>{btn_text}
                        </button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</div>

@section Scripts {{
    <partial name="_ValidationScriptsPartial" />
}}
'''
    return content

categories = ["Laptop", "Cpu", "Gpu", "Ram", "Storage"]
base_dir = r"d:\Document\WebCSharp\MVC18\MVC18\Views"

for cat in categories:
    cat_dir = os.path.join(base_dir, cat)
    if not os.path.exists(cat_dir):
        os.makedirs(cat_dir)
    
    create_path = os.path.join(cat_dir, "Create.cshtml")
    edit_path = os.path.join(cat_dir, "Edit.cshtml")
    
    with open(create_path, "w", encoding="utf-8") as f:
        f.write(create_view(cat, False))
        
    with open(edit_path, "w", encoding="utf-8") as f:
        f.write(create_view(cat, True))
        
print("Successfully generated all views for 5 products!")
