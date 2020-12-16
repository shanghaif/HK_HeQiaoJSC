<template>
  <div>
    <Card>
     		<dz-table :totalCount="stores.role.query.totalCount" :pageSize="stores.role.query.pageSize" @on-page-change="handlePageChanged"
        @on-page-size-change="handlePageSizeChanged">
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
                        v-model="stores.role.query.kw"
                        placeholder="输入关键字搜索..."
                        @on-search="handleSearchRole()">
                      </Input>
                    </FormItem>
                    <FormItem>
                      <DatePicker type="month" 
                      style="width: 180px;"
                      :value="stores.role.query.kwendtime" 
                      @on-change="stores.role.query.kwendtime=$event" 
                      format="yyyy-MM"
                      placeholder="选择时间" >
                      </DatePicker>
                    </FormItem>
                    <Button
                    v-can="'search'"
                    icon="md-search"
                    type="primary"
                    @click="handleSearchRole()"
                    title="查询">查询
                  </Button>
                  </Form>
                </Col>
                <Col span="8" class="dnc-toolbar-btns">
                  <ButtonGroup class="mr3">
                    <Button
                      v-can="'delete'"
                      class="txt-danger"
                      icon="md-trash"
                      title="删除"
                      @click="handleBatchCommand('delete')"
                    ></Button>
                    <Button icon="md-refresh" title="刷新" @click="handleRefresh"></Button>
                  </ButtonGroup>
                  <Button
                    v-can="'create'"
                    icon="md-create"
                    type="primary"
                    @click="handleShowCreateWindow"
                    title="新增绩效申报"
                  >新增绩效申报</Button>
                    <Button
                    v-can="'import'"
                    icon="md-create"
                    type="primary"
                    @click="handleExcel"
                    title="导入"
                  >科室申报导入</Button>
                </Col>
              </Row>
            </section>
          </div>
        <Table slot="table" ref="tables" :border="false" size="small" :highlight-row="true" :data="stores.role.data"
            :columns="stores.columns" @on-selection-change="handleSelectionChange" @on-refresh="handleRefresh" @on-page-change="handlePageChanged"
            @on-page-size-change="handlePageSizeChanged">
              <template slot-scope="{row,index}" slot="auditStatus">
                <span>{{getauditStatus(row.auditStatus)}}</span>
              </template>
              <template slot-scope="{ row, index }" slot="action">
                <Poptip confirm :transfer="true" title="你确定要删除吗?" @on-ok="handleDelete(row)">
                  <Tooltip placement="top" content="删除" :delay="1000" :transfer="true">
                    <Button v-can="'delete'" type="error" size="small" shape="circle" icon="md-trash"></Button>
                  </Tooltip>
                </Poptip>
                <Tooltip placement="top" content="详情" :delay="1000" :transfer="true">
                  <Button v-can="'show'" type="success" size="small" shape="circle" icon="md-search" @click="handleshow(row)"></Button>
                </Tooltip>
              <Tooltip placement="top" content="编辑" :delay="1000"  :transfer="true">
                <Button v-can="'edit'" type="primary" size="small" shape="circle" icon="md-create" @click="handleEdit(row)"></Button>
              </Tooltip>
              </template>
				</Table>

       </dz-table>
    </Card>
    
    <!--添加编辑表单-->
    <Drawer
      :title="formTitle"
      v-model="formModel.opened"
      width="400"
      :mask-closable="false"
      :mask="false"
      :styles="styles">
      <Form :model="formModel.fields" ref="formRole" :rules="formModel.rules" label-position="left">
        <FormItem label="姓名" prop="declareName" label-position="left">
          <Input v-model="formModel.fields.declareName" placeholder="请输入姓名"/>
        </FormItem>
        <FormItem label="部门" prop="declareDepartment" label-position="left">
          <Select v-model="formModel.fields.declareDepartment" placeholder="部门" filterable>
              <Option v-for="item in fuzerendata" :value="item.departmentUUID">{{item.name}}</Option>
          </Select>
        </FormItem>   
        <FormItem label="时间" prop="declareTime" label-position="left">
         <DatePicker type="month" :value="formModel.fields.declareTime" @on-change="formModel.fields.declareTime=$event" format="yyyy-MM"  placeholder="请选择时间" ></DatePicker>
        </FormItem>
        <FormItem label="加分项" prop="bonusPoint" label-position="left">
          <Input v-model="formModel.fields.bonusPoint" placeholder="请输入加分项"/>
        </FormItem>
        <FormItem label="加分分值" prop="plusScore" label-position="left">
         <!-- <Input v-model="formModel.fields.plusScore" placeholder="请输入加分分值"/> -->
          <InputNumber :max="999999" :min="1" v-model="formModel.fields.plusScore"></InputNumber>
        </FormItem>
        <FormItem label="加分内容" prop="plusContent" label-position="left">
          <Input v-model="formModel.fields.plusContent" placeholder="请输入加分内容"/>
        </FormItem>
        <FormItem label="减分项" prop="deduction" label-position="left">
          <Input v-model="formModel.fields.deduction" placeholder="请输入减分项"/>
        </FormItem>
        <FormItem label="减分分值" prop="deductionScore" label-position="left">
         <!-- <Input v-model="formModel.fields.deductionScore" placeholder="请输入减分分值"/> -->
          <InputNumber :max="999999" :min="1" v-model="formModel.fields.deductionScore"></InputNumber>
        </FormItem>
        <FormItem label="减分内容" prop="deductionContent" label-position="left">
          <Input v-model="formModel.fields.deductionContent" placeholder="请输入减分内容"/>
        </FormItem>
       <FormItem label="备注" prop="remark" label-position="top">
          <Input type="textarea" v-model="formModel.fields.remark" :rows="4" placeholder="备注"/>
        </FormItem>
      </Form>     
      <div class="demo-drawer-footer">
        <Button icon="md-checkmark-circle" type="primary" @click="handleSubmitRole">保 存</Button>
        <Button style="margin-left: 8px" icon="md-close" @click="formModel.opened = false">取 消</Button>
      </div>
    </Drawer>

    <!--导入-->
    <Drawer
        title="科室申报导入"
        v-model="formimport.opened"
        width="500"
        :mask-closable="true"
        :mask="true">
        <div>
          <input
            ref="inputer"
            id="upload"
            type="file"
            @change="importfxx"
            accept=".csv, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel"/>
          <Button
          v-can="'model'"
          icon="ios-cloud-download"
          type="warning"
          @click="handleimportmodel"
          title="绩效申报信息导入模板下载"
          >绩效申报信息导入模板下载</Button>
          <Button
            icon="md-checkmark-circle"
            type="primary"
            @click="handleimport"
            :disabled="importdisable"
          >导入</Button>
          <Tabs value="name1">
            <TabPane label="成功" name="name1" v-html="successmsg"></TabPane>
           <!-- <TabPane label="重复" name="name2" v-html="repeatmsg"></TabPane> -->
            <TabPane label="失败" name="name3" v-html="defaultmsg"></TabPane>
          </Tabs>
        </div>
    </Drawer>

    <!--详情-->
    <Drawer
	    title="详情"
	    v-model="formMode2.opened"
	    width="400"
	    :mask-closable="false"
	    :mask="false"
	    :styles="styles">
        <Form :model="formMode2.fields" ref="详情"  label-position="left">
          <FormItem label="姓名">
              <Input v-model="formMode2.fields.declareName" :readonly="true"/>
          </FormItem>
          <FormItem label="部门">
              <Input v-model="formMode2.fields.declareDepartmentName" :readonly="true"/>
          </FormItem>
          <FormItem label="时间">
              <Input v-model="formMode2.fields.declareTime" :readonly="true"/>
          </FormItem>
          <FormItem label="加分项">
              <Input v-model="formMode2.fields.bonusPoint" :readonly="true"/>
          </FormItem>
          <FormItem label="加分分值">
              <Input v-model="formMode2.fields.plusScore" :readonly="true"/>
          </FormItem>
          <FormItem label="加分内容">
              <Input v-model="formMode2.fields.plusContent" :readonly="true"/>
          </FormItem>
          <FormItem label="减分项">
              <Input v-model="formMode2.fields.deduction" :readonly="true"/>
          </FormItem>
          <FormItem label="减分分值">
              <Input v-model="formMode2.fields.deductionScore" :readonly="true"/>
          </FormItem>
          <FormItem label="减分内容">
              <Input v-model="formMode2.fields.deductionContent" :readonly="true"/>
          </FormItem>
          <FormItem label="备注" label-position="top">
            <Input type="textarea" v-model="formMode2.fields.remark" :rows="4" :readonly="true" />
          </FormItem> 
          <FormItem label="创建时间">
              <Input v-model="formMode2.fields.establishTime" :readonly="true"/>
          </FormItem>
          <FormItem label="创建人">
              <Input v-model="formMode2.fields.establishName" :readonly="true"/>
          </FormItem>
        </Form>
	  </Drawer>

  </div>
</template>

<script>
import DzTable from "_c/tables/dz-table.vue";
import config from '@/config';
import {
  getRoleList,
  createRole,
  loadRole,
  editRole,
  deleteRole,
  batchCommand,//删除
  RegistPicture,
  RubbishInfoImport,//导入
  getfuzeren//下拉框
} from "@/api/performance/performanceDeclare";
export default {
  // url:"",
  name: "rbac_role_page",
  components: {
    DzTable
  },
  data() {
    return {
      commands: {
        delete: { name: "delete", title: "删除" },
        recover: { name: "recover", title: "恢复" },
        forbidden: { name: "forbidden", title: "禁用" },
        normal: { name: "normal", title: "启用" }
      },
      //导入
      url: config.baseUrl.dev,
      importdisable: false,
      successmsg: "",
      repeatmsg: "",
      defaultmsg: "",
      formimport: {
        opened: false
      },
      //添加编辑
      formModel: {
        opened: false,
        title: "创建绩效申报",
        mode: "create",
        selection: [],
        fields: {
          id: "",
          plusScore:null,
          deductionScore:null,
          declareName:"",
          declareDepartment:"",
          declareTime:"",
        },
        rules: {
          declareName: [
            {
              type: "string",
              required: true,
              message: "请输入姓名",
              min: 2
            }
          ],
          declareDepartment: [
            {
              type: "string",
              required: true,
              message: "请输入部门",
              min: 2
            }
          ],
          declareTime: [
            {
              type: "string",
              required: true,
              message: "请输入时间",
              min: 2
            }
          ],

        }
      },
      accessoryas:"",
      //详情
      formMode2:{
        opened: false,
        title: "绩效申报详情",
        mode: "show",
        selection: [],
        fields: {
        id: "",
        },
      },
      fuzerendata:[],//负责人
      stores: {
        role: {
          query: {
            totalCount: 0,
            pageSize: 20,
            currentPage: 1,
            kw: "",
            kwendtime:"",
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
            statusSources: [
              { value: -1, text: "全部" },
              { value: 0, text: "禁用" },
              { value: 1, text: "正常" }
            ],
            statusFormSources: [
              { value: 0, text: "禁用" },
              { value: 1, text: "正常" }
            ]
          }, 
          data: []
        },
             //绑定数据
          columns: [
            { type: "selection", width: 50, key: "handle" },
            { title: "姓名", key: "declareName",ellipsis: true,tooltip: true},
            {title: "部门",ellipsis: true,tooltip: true,key: "declareDepartment"},
            { title: "时间", key: "declareTime" },
            { title: "加分项", key: "bonusPoint" },
            { title: "减分项", key: "deduction" },
            {
              title: "操作",
              align: "center",
              key: "handle",
              width: 150,
              className: "table-command-column",
    					slot: "action"
            }
          ],
      },
      styles: {
        height: "calc(100% - 55px)",
        overflow: "auto",
        paddingBottom: "53px",
        position: "static"
      }
    };
  },
  computed: {
    formTitle() {
      if (this.formModel.mode === "create") {
        return "创建绩效申报";
      }
      if (this.formModel.mode === "edit") {
        return "编辑绩效申报";
      }
      return "";
    },
    selectedRows() {
      return this.formModel.selection;
    },
    selectedRowsId() {
      return this.formModel.selection.map(x => x.departmentDeclareUuid);
    }
  },
  methods: {
  
    //点击导入
    handleExcel(params) {
      this.successmsg = "";
      this.repeatmsg = "";
      this.defaultmsg = "";
      this.$refs.inputer.value = "";
      this.isexitexcel = false;
      this.formimport.opened = true;
    },
    //导入
	  importfxx(e) {
				let inputDOM = this.$refs.inputer;
				//声明一个FormDate对象
				let formData = new FormData();
				let file = inputDOM.files[0];
				let AllUpExt = ".xls|.xlsx|";
				let extName = file.name
					.substring(file.name.lastIndexOf("."))
					.toLowerCase();
				if (AllUpExt.indexOf(extName + "|") == "-1") {
					this.$refs.inputer.value = "";
					this.$Message.warning("文件格式不正确!");
				} else {
					if (file != null && file != undefined) {
						this.isexitexcel = true;
						formData.append("excelFile", file);
						this.exceldata = formData;
					} else {
						this.isexitexcel = false;
					}
				}
	  },

    //点击导入
    async handleimport() {
				this.successmsg = "";
				this.repeatmsg = "";
				this.defaultmsg = "";
				if (this.isexitexcel) {
					await RubbishInfoImport(this.exceldata).then(res => {
						// console.log(res.data);
						if (res.data.code == 200) {
							this.time = res.data.data.time;
							this.successmsg = res.data.data.successmsg;
							this.repeatmsg = res.data.data.repeatmsg;
							this.defaultmsg = res.data.data.defaultmsg;
						  this.loadRoleList();
						} else {
							this.$Message.warning(res.data.message);
						}
						this.$refs.inputer.value = "";
						this.exceldata = new FormData();
						this.isexitexcel = false;
					});
				} else {
					this.$Message.warning("请选择文件");
				}
				// this.importdisable = false;
	  },

     //下载导入模版
    handleimportmodel() {
      window.location.href=this.url + "UploadFiles/PerformanceDeclare/绩效申报导入模板.xlsx";
    },

     //下载附件
    handleimportmode2(){
      window.location.href=this.url+"UploadFiles/PersonalDiary/"+this.accessoryas
    },

    //部门下拉框
	  loadfuzeren(){
        getfuzeren().then(res => {
        this.fuzerendata = res.data.data
      });
	  },
   
    //查询数据列表
    loadRoleList() {
      getRoleList(this.stores.role.query).then(res => {
        this.stores.role.data = res.data.data;
        this.stores.role.query.totalCount = res.data.totalCount;
      });
    },
    //打开创建绩效申报右侧导航
    handleOpenFormWindow() {
      this.formModel.opened = true;
    },
    //关闭创建绩效申报右侧导航
    handleCloseFormWindow() {
      this.formModel.opened = false;
    },
    //创建绩效申报页面
    handleSwitchFormModeToCreate() {
      this.formModel.mode = "create";
    },
    //编辑绩效申报页面
    handleSwitchFormModeToEdit() {
      this.formModel.mode = "edit";
      this.handleOpenFormWindow();
    },
    //编辑绩效申报页面
    handleEdit(params) {
      this.handleSwitchFormModeToEdit();
      this.handleResetFormRole();
      this.doLoadRole(params.departmentDeclareUuid);
    },

    //详情绩效申报页面
    handleshow(params) {
      this.handleSwitchFormModeToShow();
      this.handleResetFormRole();
      this.doLoadShow(params.departmentDeclareUuid);
    },
    //详情绩效申报页面
    handleSwitchFormModeToShow() {
      this.formMode2.mode = "show";
      this.handleOpenFormShow();
    },
    //打开详情绩效申报右侧导航
    handleOpenFormShow() {
      this.formMode2.opened = true;
    },
    //详情绑定数据
    doLoadShow(departmentDeclareUuid) {
      loadRole({ guid: departmentDeclareUuid}).then(res => {
        this.formMode2.fields = res.data.data;
       this.accessoryas=this.formMode2.fields.accessory;
      });
    },

    handleSelect(selection, row) {},
    handleSelectionChange(selection) {
      this.formModel.selection = selection;
    },
    //刷新
    handleRefresh() {
      this.loadRoleList();
    },
    //新增日志
    handleShowCreateWindow() {
      this.handleSwitchFormModeToCreate();
      this.handleOpenFormWindow();
      this.handleResetFormRole();
    },

    //保存
    handleSubmitRole() {
      let valid = this.validateRoleForm();
      if (valid) {
        if (this.formModel.mode === "create") {
          this.doCreateRole();
        }
        if (this.formModel.mode === "edit") {
          this.doEditRole();
        }
      }
    },

    handleResetFormRole() {
      this.$refs["formRole"].resetFields();
    },

    //添加
    doCreateRole() {
      createRole(this.formModel.fields).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadRoleList();
          this.handleCloseFormWindow();//关闭
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    //编辑
    doEditRole() {
      editRole(this.formModel.fields).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadRoleList();
          this.handleCloseFormWindow();//关闭
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },

    //提交非空验证
    validateRoleForm() {
      let _valid = false;
      this.$refs["formRole"].validate(valid => {
        if (!valid) {
          this.$Message.error("请完善表单信息");
          _valid = false;
        } else {
          _valid = true;
        }
      });
      return _valid;
    },
    //绑定编辑数据
    doLoadRole(departmentDeclareUuid) {
      loadRole({ guid: departmentDeclareUuid}).then(res => {
        this.formModel.fields = res.data.data;
      });
    },
    handleDelete(params) {
      this.doDelete(params.departmentDeclareUuid);
    },
    doDelete(ids) {
      if (!ids) {
        this.$Message.warning("请选择至少一条数据");
        return;
      }
      deleteRole(ids).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadRoleList();
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    //批量操作
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
    //批量操作
    doBatchCommand(command) {
      batchCommand({
        command: command,
        ids: this.selectedRowsId.join(",")
      }).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadRoleList();
          this.formModel.selection=[];
        } else {
          this.$Message.warning(res.data.message);
        }
        this.$Modal.remove();
      });
    },
    handleSearchRole() {
      this.loadRoleList();
    },
    rowClsRender(row, index) {
      if (row.isDeleted) {
        return "table-row-disabled";
      }
      return "";
    },
    //分页
    handlePageChanged(page) {
      this.stores.role.query.currentPage = page;
      this.loadRoleList();
    },
    handlePageSizeChanged(pageSize) {
      this.stores.role.query.pageSize = pageSize;
      this.loadRoleList();
    }
  },
  mounted() {
    this.loadRoleList();
    this.loadfuzeren();//部门下来框
    // this.url=config.baseUrl.dev;
  }
};
</script>
