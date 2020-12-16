<template>
  <div>
    <Card>
      <dz-table
        :totalCount="stores.department.query.totalCount"
        :pageSize="stores.department.query.pageSize"
        @on-page-change="handlePageChanged"
        @on-page-size-change="handlePageSizeChanged"
      >
        <div slot="searcher">
          <section class="dnc-toolbar-wrap">
            <Row :gutter="16">
              <Col span="16">
                <Form inline @submit.native.prevent>
                  <FormItem>
                    <Input
                      type="text"
                      search
                      :clearable="true"
                      v-model="stores.department.query.kw"
                      placeholder="输入关键字搜索..."
                      @on-search="handleSearchDepartment()"
                    >
                      <Select
                        slot="prepend"
                        v-model="stores.department.query.isDeleted"
                        @on-change="handleSearchDepartment"
                        placeholder="删除状态"
                        style="width:60px;"
                      >
                        <Option
                          v-for="item in stores.department.sources.isDeletedSources"
                          :value="item.value"
                          :key="item.value"
                        >{{item.text}}</Option>
                      </Select>
                    </Input>
                  </FormItem>
                </Form>
              </Col>
              <Col span="8" class="dnc-toolbar-btns">
                <ButtonGroup class="mr3">
                  <Button
                    class="txt-danger"
                    icon="md-trash"
                    title="删除"
                    @click="handleBatchCommand('delete')"
                  ></Button>
                  <Button
                    class="txt-success"
                    icon="md-redo"
                    title="恢复"
                    @click="handleBatchCommand('recover')"
                  ></Button>
                  <Button icon="md-refresh" title="刷新" @click="handleRefresh"></Button>
                </ButtonGroup>
                <Button
                  v-can="'create'"
                  icon="md-create"
                  type="primary"
                  @click="handleShowCreateWindow"
                  title="新增科室"
                >新增科室</Button>
              </Col>
            </Row>
          </section>
        </div>
        <Table
          slot="table"
          ref="tables"
          :border="false"
          size="small"
          :highlight-row="true"
          :data="stores.department.data"
          :columns="stores.department.columns"
          @on-select="handleSelect"
          @on-selection-change="handleSelectionChange"
          @on-refresh="handleRefresh"
          :row-class-name="rowClsRender"
          @on-page-change="handlePageChanged"
          @on-page-size-change="handlePageSizeChanged"
          @on-sort-change="handleSortChange"
        >
          <template slot-scope="{ row, index }" slot="action">
            <Poptip
              confirm
              :transfer="true"
              title="确定要删除吗?"
              @on-ok="handleDelete(row)"
            >
              <Tooltip placement="top" content="删除" :delay="1000" :transfer="true">
                <Button type="error" size="small" shape="circle" icon="md-trash"></Button>
              </Tooltip>
            </Poptip>
            <Tooltip placement="top" content="编辑" :delay="1000" :transfer="true">
              <Button v-can="'edit'" type="primary" size="small" shape="circle" icon="md-create" @click="handleEdit(row)"></Button>
            </Tooltip>

          </template>
        </Table>
      </dz-table>
    </Card>
    <Drawer
      :title="formTitle"
      v-model="formModel.opened"
      width="400"
      :mask-closable="false"
      :mask="false"
      :styles="styles"
    >
      <Form :model="formModel.fields" ref="formDepartment" :rules="formModel.rules" label-position="left">
        <FormItem label="科室名称" prop="departmentName" label-position="left">
          <Input v-model="formModel.fields.departmentName" placeholder="请输入科室名称"/>
        </FormItem>
      </Form>
      <div class="demo-drawer-footer">
        <Button icon="md-checkmark-circle" type="primary" @click="handleSubmitDepartment">保 存</Button>
        <Button style="margin-left: 8px" icon="md-close" @click="formModel.opened = false">取 消</Button>
      </div>
    </Drawer>
  </div>
</template>

<script>
    import DzTable from "_c/tables/dz-table.vue";
    import {
        getDepartmentList,
        createDepartment,
        loadDepartment,
        editDepartment,
        deleteDepartment,
        batchCommand
    } from "@/api/rbac/department";
    export default {
        name: "consumable",
        components: {
            DzTable
        },
        data() {
            return {
                commands: {
                    delete: { name: "delete", title: "删除" },
                    recover: { name: "recover", title: "恢复" },
                },
                formModel: {
                    opened: false,
                    title: "创建科室",
                    mode: "create",
                    selection: [],
                    fields: {
                        departmentName: "",
                    },
                    rules: {
                        departmentName: [
                            { type: "string", required: true, message: "请输入科室名", min: 3 }
                        ],
                    }
                },
                stores: {
                    department: {
                        query: {
                            totalCount: 0,
                            pageSize: 20,
                            currentPage: 1,
                            kw: "",
                            isDeleted: 0,
                            status: -1,
                            sort: [
                                {
                                    direct: "DESC",
                                    field: "ID"
                                }
                            ]
                        },
                        sources: {
                            isDeletedSources: [
                                { value: -1, text: "全部" },
                                { value: 0, text: "正常" },
                                { value: 1, text: "已删" }
                            ],
                        },
                        columns: [
                            { type: "selection", width: 50, key: "handle" },
                            { title: "科室名", key: "departmentName"},

                            { title: "操作", align: "center", width: 150, className: "table-command-column",slot:"action" }
                        ],
                        data: []
                    }
                },
                styles: {
                    height: "calc(100% - 55px)",
                    overflow: "auto",
                    paddingBottom: "53px",
                    position: "static"
                },
            };
        },
        computed: {
            formTitle() {
                if (this.formModel.mode === "create") {
                    return "创建用户";
                }
                if (this.formModel.mode === "edit") {
                    return "编辑用户";
                }
                return "";
            },
            selectedRows() {
                return this.formModel.selection;
            },
            selectedRowsId() {
                return this.formModel.selection.map(x => x.departmentUuid);
            }
        },
        methods:{
            loaddepartmentList() {
                getDepartmentList(this.stores.department.query).then(res => {
                    this.stores.department.data = res.data.data;
                    this.stores.department.query.totalCount = res.data.totalCount;
                });
            },
            handleOpenFormWindow() {
                this.formModel.opened = true;
            },
            handleCloseFormWindow() {
                this.formModel.opened = false;
            },
            handleSwitchFormModeToCreate() {
                this.formModel.mode = "create";
            },
            handleSwitchFormModeToEdit() {
                this.formModel.mode = "edit";
                this.handleOpenFormWindow();
            },
            handleEdit(row) {
                this.handleSwitchFormModeToEdit();
                this.handleResetFormDepartment();
                this.doLoadDepartment(row.departmentUuid);
            },
            handleSelect(selection, row) {},
            handleSelectionChange(selection) {
                this.formModel.selection = selection;
            },
            handleRefresh() {
                this.loaddepartmentList();
            },
            handleShowCreateWindow() {
                this.handleSwitchFormModeToCreate();
                this.handleOpenFormWindow();
                this.handleResetFormDepartment();
            },
            handleSubmitDepartment() {
                let valid = this.validateDepartmentForm();
                //console.log(valid);
                //console.log(this.formModel.fields);
                if (valid) {
                    if (this.formModel.mode === "create") {
                        this.doCreateDepartment();
                    }
                    if (this.formModel.mode === "edit") {
                        this.doEditDepartment();
                    }
                }
            },
            handleResetFormDepartment() {
                this.$refs["formDepartment"].resetFields();
            },
            doCreateDepartment() {
                createDepartment(this.formModel.fields).then(res => {
                    if (res.data.code === 200) {
                        this.$Message.success(res.data.message);
                        this.handleCloseFormWindow();
                        this.loaddepartmentList();
                    } else {
                        this.$Message.warning(res.data.message);
                    }
                });
            },
            doEditDepartment() {
                editDepartment(this.formModel.fields).then(res => {
                    if (res.data.code === 200) {
                        this.$Message.success(res.data.message);
                        this.handleCloseFormWindow();
                        this.loaddepartmentList();
                    } else {
                        this.$Message.warning(res.data.message);
                    }
                });
            },
            validateDepartmentForm() {
                let _valid = false;
                this.$refs["formDepartment"].validate(valid => {
                    if (!valid) {
                        this.$Message.error("请完善表单信息");
                    } else {
                        _valid = true;
                    }
                });
                return _valid;
            },
            doLoadDepartment(departmentUuid) {
                loadDepartment({ guid: departmentUuid}).then(res => {
                    this.formModel.fields = res.data.data;
                });
            },
            handleDelete(row) {
                this.doDelete(row.departmentUuid);
            },
            doDelete(ids) {
                if (!ids) {
                    this.$Message.warning("请选择至少一条数据");
                    return;
                }
                deleteDepartment(ids).then(res => {
                    if (res.data.code === 200) {
                        this.$Message.success(res.data.message);
                        this.loaddepartmentList();
                        this.formModel.selection = [];
                    } else {
                        this.$Message.warning(res.data.message);
                    }
                });
            },
            handleBatchCommand(command) {
                if (!this.selectedRowsId || this.selectedRowsId.length <= 0) {
                    this.$Message.warning("请选择至少一条数据");
                    return;
                }
                this.$Modal.confirm({
                    title: "操作提示",
                    content:
                        "<p>确定要执行当前 [" +
                        this.commands[command].title +
                        "] 操作吗?</p>",
                    loading: true,
                    onOk: () => {
                        this.doBatchCommand(command);
                    }
                });
            },
            doBatchCommand(command) {
                batchCommand({
                    command: command,
                    ids: this.selectedRowsId.join(",")
                }).then(res => {
                    if (res.data.code === 200) {
                        this.$Message.success(res.data.message);
                        this.loaddepartmentList();
                        this.formModel.selection = [];
                    } else {
                        this.$Message.warning(res.data.message);
                    }
                    this.$Modal.remove();
                });
            },
            handleSearchDepartment() {
                this.loaddepartmentList();
            },
            rowClsRender(row, index) {
                if (row.isDeleted) {
                    return "table-row-disabled";
                }
                return "";
            },
        },
        mounted() {
            this.loaddepartmentList();
        }
    }
</script>

<style scoped>

</style>
